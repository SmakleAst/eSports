﻿using eSports.Domain.Teams.Entity;
using eSports.Domain.Teams.Filter;
using eSports.Domain.Teams.Response;
using eSports.Domain.Teams.ViewModels;

namespace eSports.Service.Teams.Interfaces
{
    public interface ITeamService
    {
        Task<ITeamResponse<TeamEntity>> Create(CreateTeamViewModel model);

        Task<ITeamResponse<TeamEntity>> Delete(TeamViewModel model);

        Task<ITeamResponse<TeamEntity>> Update(TeamViewModel model);

        Task<ITeamResponse<IEnumerable<TeamViewModel>>> GetAllTeams(TeamFilter filter);
    }
}