using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PersonalFinances.Application.Interfaces;
using PersonalFinances.Application.ViewModel.Request.Expenditure;
using PersonalFinances.Application.ViewModel.Response;
using PersonalFinances.Domain.Entities;
using PersonalFinances.Domain.Exceptions;
using PersonalFinances.Domain.Interfaces;

namespace PersonalFinances.Application.Services
{
    public class ExpenditureService : IExpenditureService
    {
        private IExpenditureRepository _expenditureRepository;
        private IMapper _mapper;

        public ExpenditureService(IMapper mapper, IExpenditureRepository expenditureRepository)
        {
            _mapper = mapper;
            _expenditureRepository = expenditureRepository;
        }


        public async Task<ExpenditureResponse> Create(CreateExpenditureRequest createExpenditureRequest, int userId)
        {
            Expenditure expenditure = new Expenditure(createExpenditureRequest.Name, createExpenditureRequest.ExpenditureCategoryId, createExpenditureRequest.Date,
                                                      createExpenditureRequest.Value, createExpenditureRequest.Description, userId);
            await _expenditureRepository.AddAsync(expenditure);

            ExpenditureResponse expenditureResponse = _mapper.Map<ExpenditureResponse>(expenditure);

            return expenditureResponse;
        }

        public async Task<ExpenditureResponse> Delete(int id, int userId)
        {
            Expenditure expenditure = await _expenditureRepository.GetByIdAndUserIdAsync(id, userId);

            if (expenditure == null)
                throw new BusinessException("Invalid Id");

            await _expenditureRepository.DeleteAsync(expenditure);

            ExpenditureResponse expenditureResponse = _mapper.Map<ExpenditureResponse>(expenditure);

            return expenditureResponse;
        }

        public async Task<List<ExpenditureResponse>> GetByUserId(int userId)
        {
            List<Expenditure> expenditures = (List<Expenditure>) await _expenditureRepository.GetByUserIdAsync(userId);
            List<ExpenditureResponse> expenditureResponses = _mapper.Map<List<ExpenditureResponse>>(expenditures); 

            return expenditureResponses;
        }

        public async Task<ExpenditureResponse> GetByIdAndUserId(int id, int userId)
        {
            Expenditure expenditure = (Expenditure) await _expenditureRepository.GetByIdAndUserIdAsync(id, userId);
            ExpenditureResponse expenditureResponse = _mapper.Map<ExpenditureResponse>(expenditure); 

            return expenditureResponse;
        }

        public async Task<ExpenditureResponse> Update(UpdateExpenditureRequest updateExpenditureRequest, int id, int userId)
        {
            Expenditure expenditure = await _expenditureRepository.GetByIdAndUserIdAsync(id, userId);

            if (expenditure == null)
                throw new BusinessException("Invalid Id");

            expenditure.Update(updateExpenditureRequest.Name, updateExpenditureRequest.ExpenditureCategoryId, updateExpenditureRequest.Date,
                               updateExpenditureRequest.Value, updateExpenditureRequest.Description);

            await _expenditureRepository.UpdateAsync(expenditure);

            ExpenditureResponse expenditureResponse = _mapper.Map<ExpenditureResponse>(expenditure);

            return expenditureResponse;
        }
    }
}