using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.ServiceModels;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Service.Interfaces
{
    public interface IPcrService
    {
        Task<SpaceAllocation> CreateAndAllocateSpace(SpaceAllocationModel allocationModel);
        Task SetAvailability(Guid space_id, bool isAvailable);
        Task<IEnumerable<SpaceAllocation>> GetAllAvalableSpace();
        Task BookSpace(BookingModel bookingModel);
        Task<IEnumerable<BookingDetail>> Getbookings();
        Task Cancelbooking(string userId, Guid bookingId);
        Task SetLabResult(string userId, Guid bookingId, TestStatus status);
        Task<IEnumerable<BookingDetail>> GetbookingsByUser(string userId);
        Task<ReportModel> Report();
    }
}
