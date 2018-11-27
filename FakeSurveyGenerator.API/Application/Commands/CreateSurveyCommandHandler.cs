using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using FakeSurveyGenerator.API.Application.Models;
using FakeSurveyGenerator.Domain.AggregatesModel.AuditAggregate;
using FakeSurveyGenerator.Domain.AggregatesModel.SurveyAggregate;
using FakeSurveyGenerator.Domain.Services;

namespace FakeSurveyGenerator.API.Application.Commands
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, SurveyModel>
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;

        public CreateSurveyCommandHandler(ISurveyRepository surveyRepository, IMapper mapper, IAuditRepository auditRepository)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
            _auditRepository = auditRepository;
        }

        public async Task<SurveyModel> Handle(CreateSurveyCommand command, CancellationToken cancellationToken)
        {
            var survey = new Survey(command.SurveyTopic, command.NumberOfRespondents, command.RespondentType);

            foreach (var option in command.SurveyOptions)
            {
                if (option.PreferredOutcomeRank.HasValue)
                    survey.AddSurveyOption(option.OptionText, option.PreferredOutcomeRank.Value);
                else
                    survey.AddSurveyOption(option.OptionText);
            }

            var voteDistributionStrategy = new OneSidedVoteDistributionStrategy();

            var result = survey.CalculateOutcome(voteDistributionStrategy);

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var insertedSurvey = _surveyRepository.Add(result);

                _auditRepository.Add(new Audit
                {
                    WhatHappened = "Added Survey",
                    When = DateTime.UtcNow
                });

                await _surveyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                scope.Complete();

                return _mapper.Map<SurveyModel>(insertedSurvey);
            }
        }
    }
}
