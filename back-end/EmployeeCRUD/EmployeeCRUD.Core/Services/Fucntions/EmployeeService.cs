using AutoMapper;
using EmployeeCRUD.Core.DTOs;
using EmployeeCRUD.Core.DTOs.Common;
using EmployeeCRUD.Core.Entities;
using EmployeeCRUD.Core.Reopsitories;
using EmployeeCRUD.Core.Services.Contracts;
using EmployeeCRUD.Core.Services.Contracts.Genaric;
using EmployeeCRUD.Core.Utilities.LinqBuilder.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCRUD.Core.Services.Fucntions;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeeService(
        IUnitOfWork unitOfWork,
        IEmployeeRepository repository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<ServiceResponse<bool>> Delete(int Id)
    {
        var dbRow = await _repository.Select(x => x.Id == Id, i => i)
            .SingleOrDefaultAsync();
        _repository.Remove(dbRow);
        await _unitOfWork.CommitAsync();
        return new ServiceResponse<bool>(ServiceResponseCode.Success, true);
    }

    public async Task<ServiceResponse<PageingDataResponse<EmployeeListItem>>> Get(DataPagingRequest request)
    {
        int PageNumber = 0;
        if (request.skip > 0)
            PageNumber = request.skip / request.take;
        string SoringColumn = "Id";
        string SortDirection = "asc";
        TextInfo _textInfo = new CultureInfo("en-US", false).TextInfo;
        if (request.Sort != null && request.Sort.Count > 0)
        {
            SoringColumn = _textInfo.ToTitleCase(request.Sort[0].field);
            SortDirection = request.Sort[0].dir;
        }
        var predicate = request.BuildInLinq<Employee>();
        if (!string.IsNullOrWhiteSpace(request.ExtraFilter))
            predicate = predicate.And(P => P.Name == request.ExtraFilter || P.Address.Contains(request.ExtraFilter));
        var data = await _repository
                         .GetPaging(predicate,
                                item => _mapper.Map<EmployeeListItem>(item),
                                SoringColumn,
                                request.take,
                                PageNumber,
                                SortDirection);
        return new ServiceResponse<PageingDataResponse<EmployeeListItem>>(ServiceResponseCode.Success, data);
    }

    public async Task<ServiceResponse<EmployeeDTO>> Get(int Id)
    {
        var data = await _repository.Select(x => x.Id == Id,
            i => _mapper.Map<EmployeeDTO>(i))
            .SingleOrDefaultAsync();
        return new ServiceResponse<EmployeeDTO>(data == null ? ServiceResponseCode.NotFoundData : ServiceResponseCode.Success, data);
    }

    public async Task<ServiceResponse<EmployeeDTO>> Save(EmployeeDTO dto)
    {
        if (dto.Id > 0)
        {
            var dbRow = await _repository.Select(x => x.Id == dto.Id, i => i)
            .SingleOrDefaultAsync();
            var model = _mapper.Map(dto, dbRow);
            _repository.Update(model);
        }
        else
        {
            var model = _mapper.Map<Employee>(dto);
            _repository.Add(model);
        }
        int ar = await _unitOfWork.CommitAsync();
        return new ServiceResponse<EmployeeDTO>(ServiceResponseCode.Success, dto);
    }
}
