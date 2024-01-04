using eshop.Frontend.Web.Models;

namespace eshop.Frontend.Web.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}
