using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthPetApp
{
    abstract class BasePageViewModel : BaseViewModel
    {
        protected BasePageViewModel()
        {
            
        }
        //internal static async Task<UserRegisterAccountModel> GetCacheDataRegisterUser()
        //{
        //    var userData = await CacheService.Current.GetCacheAsync(MessageKeys.UserRegisterData);

        //    if (userData is null) return new UserRegisterAccountModel();

        //    var userDataFormated = JsonSerializer.Deserialize<UserRegisterAccountModel>(userData);

        //    return userDataFormated is not null ? userDataFormated : new UserRegisterAccountModel();
        //}

        //internal static async Task SetCacheDataRegisterUser(UserRegisterAccountModel userData) =>
        //    await CacheService.Current.SetCacheAsync(MessageKeys.UserRegisterData, JsonSerializer.Serialize(userData));
    }
}
