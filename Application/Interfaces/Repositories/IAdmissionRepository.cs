using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IAdmissionRepository
    {
        Task<int> CreateAsync(Admission admission);
    }
}
