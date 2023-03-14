using Microsoft.AspNetCore.Mvc;

namespace AgendaUpc.ViewComponents;

public class MenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult((IViewComponentResult) View());
    }
}