using Microsoft.AspNetCore.Mvc;

namespace MoreNet.DependencyInjection.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private INamedServiceContainer<IAddNamedScoped> _forAddNamedScoped;

        public DemoController(INamedServiceContainer<IAddNamedScoped> forAddNamedScoped)
        {
            _forAddNamedScoped = forAddNamedScoped;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var forAddNamedScoped1 = _forAddNamedScoped.GetService("N1");
            var forAddNamedScoped2 = _forAddNamedScoped.GetService("N2");
            var output =
@$"
Inteface: {nameof(IAddNamedScoped)}, Implementation: {forAddNamedScoped1.GetType()}, Name: {forAddNamedScoped1.Name}
Inteface: {nameof(IAddNamedScoped)}, Implementation: {forAddNamedScoped2.GetType()}, Name: {forAddNamedScoped2.Name}
";
            return Ok(output);
        }
    }

    public interface IAddNamedScoped : INameable { }

    public class AddNamedScoped1 : IAddNamedScoped
    {
        public string Name => "N1";
    }

    public class AddNamedScoped2 : IAddNamedScoped
    {

        public string Name => "N2";
    }
}
