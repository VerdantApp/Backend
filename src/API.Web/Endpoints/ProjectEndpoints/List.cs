using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Verdant.API.Core.ProjectAggregate;
using Verdant.API.Web.Endpoints.ProjectEndpoints;
using Verdant.API.SharedKernel.Interfaces;

namespace Verdant.API.Web.Endpoints.ProjectEndpoints;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<ProjectListResponse>
{
  private readonly IReadRepository<Project> _repository;

  public List(IReadRepository<Project> repository)
  {
    _repository = repository;
  }

  [HttpGet("/Projects")]
  [SwaggerOperation(
      Summary = "Gets a list of all Projects",
      Description = "Gets a list of all Projects",
      OperationId = "Project.List",
      Tags = new[] { "ProjectEndpoints" })
  ]
  public override async Task<ActionResult<ProjectListResponse>> HandleAsync(CancellationToken cancellationToken)
  {
    var response = new ProjectListResponse();
    response.Projects = (await _repository.ListAsync()) // TODO: pass cancellation token
        .Select(project => new ProjectRecord(project.Id, project.Name))
        .ToList();

    return Ok(response);
  }
}
