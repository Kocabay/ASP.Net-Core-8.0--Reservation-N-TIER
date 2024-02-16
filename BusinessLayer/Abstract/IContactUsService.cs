using EntityLayer.Concrete;
 

namespace BusinessLayer.Abstract
{
    public interface IContactUsService:IGenericService<ContactUs>
    {
        List<ContactUs> TGetListContactUsByTrue();
        List<ContactUs> TGetListContactUsByFalse();
        void TContactUsStatusChangeToFalse(int id);
    }
}
