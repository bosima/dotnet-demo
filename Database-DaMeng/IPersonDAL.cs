public interface IPersonDAL{
    int Add(PersonModel model);
    bool Update(PersonModel model);
    bool Delete(int id);
    PersonModel Get(int id);
    List<PersonModel> GetList();
}