using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Thynk_Code.Data.Interfaces;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.ServiceModels;
using Thynk_Code.Service.Interfaces;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Service.Implementations
{
    public class PcrService : BaseService, IPcrService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public PcrService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork, IServiceFactory serviceFactory) :base(unitOfWork, serviceFactory)
        {
            _userManager = userManager;
        }

        public async Task<SpaceAllocation> CreateAndAllocateSpace(SpaceAllocationModel allocationModel)
        {

            //checking if user exist
            ApplicationUser user = await _userManager.FindByIdAsync(allocationModel.userId);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            //checking if user can perform this operation
            if (!user.Role.Contains(Role.Admin.ToString()))
                throw new InvalidOperationException("Only admins can perform this operation");

            //creating space for the test
            SpaceAllocation spaceAllocationCheck = await _unitOfWork.GetRepository<SpaceAllocation>().SingleAsync(p => p.TestDate.Date == allocationModel.testDate.Date && p.time == allocationModel.time);
            if (spaceAllocationCheck != null)
                throw new InvalidOperationException("Space allocation already created");

            SpaceAllocation spaceAllocation = new SpaceAllocation
            {
                AllocatedBy = user.Id,
                EntityStatus = EntityStatus.Active,
                Location = allocationModel.location,
                TestDate = allocationModel.testDate,
                time = allocationModel.time

            };
            spaceAllocation = _unitOfWork.GetRepository<SpaceAllocation>().Add(spaceAllocation);
            await _unitOfWork.SaveChangesAsync();
            return spaceAllocation;

        }
        public async Task SetAvailability(Guid space_id, bool isAvailable)
        {

            SpaceAllocation spaceAllocationCheck = await _unitOfWork.GetRepository<SpaceAllocation>().SingleAsync(p => p.Id == space_id, disableTracking: false);
            if (spaceAllocationCheck == null)
                throw new InvalidOperationException("Space with id not found!");

            if (isAvailable)
            {
                spaceAllocationCheck.EntityStatus = EntityStatus.Active;
            }
            else
            {
                spaceAllocationCheck.EntityStatus = EntityStatus.Inactive;
            }
            await _unitOfWork.SaveChangesAsync();

        }


        public async Task<IEnumerable<SpaceAllocation>> GetAllAvalableSpace()
        {

            IEnumerable<SpaceAllocation> spaceAllocations = await _unitOfWork.GetRepository<SpaceAllocation>().GetListAsync(p=>p.EntityStatus == EntityStatus.Active);
            return spaceAllocations;
        }

        public async Task<IEnumerable<BookingDetail>> Getbookings()
        {

            IEnumerable<BookingDetail> bookingDetail = await _unitOfWork.GetRepository<BookingDetail>().GetListAsync(p => p.TestStatus == TestStatus.Pending,null,include:x=>x.Include(a=>a.User));
            return bookingDetail;
        }

        public async Task<IEnumerable<BookingDetail>> GetbookingsByUser(string userId)
        {

            IEnumerable<BookingDetail> bookingDetail = await _unitOfWork.GetRepository<BookingDetail>().GetListAsync(p => p.UserId == userId, null, include: x => x.Include(a => a.User));
            return bookingDetail;
        }

        public async Task BookSpace(BookingModel bookingModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(bookingModel.userId);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            SpaceAllocation spaceAllocationAvailableCheck = await _unitOfWork.GetRepository<SpaceAllocation>().SingleAsync(p => p.Id == bookingModel.space_id, disableTracking: false);
            if (spaceAllocationAvailableCheck == null)
                throw new InvalidOperationException("Space with id not found!");

            if (spaceAllocationAvailableCheck.EntityStatus == EntityStatus.Inactive)
                throw new InvalidOperationException("Space is not available");

            BookingDetail bookingDetailCheck = await _unitOfWork.GetRepository<BookingDetail>().SingleAsync(p => p.UserId == bookingModel.userId && p.TestDate.Date == bookingModel.testDate.Date && p.time == bookingModel.time && p.IsCanceld == false);
            if (bookingDetailCheck != null)
                throw new InvalidOperationException("User already booked a space for the date and time specified");
            BookingDetail bookingDetail = new BookingDetail
            {
                EntityStatus = EntityStatus.Active,
                IsCanceld = false,
                UserId = bookingModel.userId,
                spaceAllocationId = spaceAllocationAvailableCheck.Id,
                TestDate = bookingModel.testDate,
                TestStatus = TestStatus.Pending,
                time = bookingModel.time

            };
            bookingDetail = _unitOfWork.GetRepository<BookingDetail>().Add(bookingDetail);
            spaceAllocationAvailableCheck.EntityStatus = EntityStatus.Inactive;
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task Cancelbooking(string userId,Guid bookingId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            BookingDetail bookingDetailCheck = await _unitOfWork.GetRepository<BookingDetail>().SingleAsync(p => p.Id == bookingId, disableTracking: false);
            if (bookingDetailCheck == null)
                throw new InvalidOperationException("booking do not exist");

            if (bookingDetailCheck.TestStatus != TestStatus.Pending)
                throw new InvalidOperationException("cannot cancel booking because the test have been conducted ");

            SpaceAllocation spaceAllocationCheck = await _unitOfWork.GetRepository<SpaceAllocation>().SingleAsync(p => p.Id == bookingDetailCheck.spaceAllocationId,disableTracking: false);
            if (spaceAllocationCheck == null)
                throw new InvalidOperationException("Space with id not found!");
            bookingDetailCheck.IsCanceld = true;
            spaceAllocationCheck.EntityStatus = EntityStatus.Active;
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task SetLabResult(string userId, Guid bookingId, TestStatus status)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("User not found!");

            //checking if user can perform this operation
            if (!user.Role.Contains(Role.Lab_Admin.ToString()))
                throw new InvalidOperationException("Only Lab admins can perform this operation");

            BookingDetail bookingDetailCheck = await _unitOfWork.GetRepository<BookingDetail>().SingleAsync(p => p.Id == bookingId, disableTracking: false);
            if (bookingDetailCheck == null)
                throw new InvalidOperationException("booking do not exist");

            bookingDetailCheck.TestStatus = status;
            await _unitOfWork.SaveChangesAsync();

        }
        public async Task<ReportModel> Report()
        {
            IEnumerable<SpaceAllocation> spaceAllocations = await _unitOfWork.GetRepository<SpaceAllocation>().GetListAsync(p => p.EntityStatus == EntityStatus.Active);
            IEnumerable<BookingDetail> bookingDetail = await _unitOfWork.GetRepository<BookingDetail>().GetListAsync(p => p.EntityStatus == EntityStatus.Active && !p.IsCanceld);
            ReportModel reportModel = new ReportModel
            {
                bookingCapacity = spaceAllocations.Count(),
                bookings = bookingDetail.Count(),
                test = bookingDetail.Where(p => p.TestStatus != TestStatus.Pending).Count(),
                negativeCases = bookingDetail.Where(p => p.TestStatus == TestStatus.Negative).Count(),
                positiveCases = bookingDetail.Where(p => p.TestStatus == TestStatus.Positive).Count(),
            };

            return reportModel;


        }

    }
}
