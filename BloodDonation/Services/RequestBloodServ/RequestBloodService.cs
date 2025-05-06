using BloodDonation.Models;
using BloodDonation.Services.GenericServ;

namespace BloodDonation.Services.RequestBloodServ
{
    public class RequestBloodService : Service<RequestBlood> , IRequestBloodService
    {
        private readonly Context context;

        public RequestBloodService(Context context)
        {
            this.context = context;
        }


    }
}
