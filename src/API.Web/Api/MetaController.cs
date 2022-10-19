using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Verdant.API.Web.Api;

public class MetaController : BaseApiController
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  private readonly ILogger<MetaController> _logger;

  public MetaController(
    IHttpContextAccessor httpContextAccessor,
    ILogger<MetaController> logger)
  {
    _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  /// <summary>
  /// A sample API Controller. Consider using API Endpoints (see Endpoints folder) for a more SOLID approach to building APIs
  /// https://github.com/ardalis/ApiEndpoints
  /// </summary>
  [HttpGet("/info")]
  public ActionResult<string> Info()
  {
    var assembly = typeof(WebMarker).Assembly;

    var creationDate = System.IO.File.GetCreationTime(assembly.Location);
    var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

    var context = _httpContextAccessor.HttpContext;
    var activity = context.Features.Get<IHttpActivityFeature>()?.Activity;
    activity?.SetTag("VerdantAppVersion", version);

    _logger.LogInformation("Version: {AppVersion}, Last Updated: {CreationDate}", version, creationDate);

    return Ok($"Version: {version}, Last Updated: {creationDate}");
  }
}
