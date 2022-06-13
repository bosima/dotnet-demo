using Dm;

public class PersonAdoNetDAL : IPersonDAL
{
    static readonly DmDbHelper _client = new DmDbHelper("Server=127.0.0.1; UserId=TESTDB; PWD=1234567");

    public int Add(PersonModel model)
    {
        string sql = "insert into Person(Name,City) Values(:Name,:City)";
        DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Name",model.Name),
                new DmParameter(":City",model.City)
            };

        return _client.ExecuteAdd(sql, paras);
    }

    public bool Update(PersonModel model)
    {
        string sql = "update Person set City=:City where Id=:Id";
        DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",model.Id),
                new DmParameter(":City",model.City)
            };

        return _client.ExecuteSql(sql, paras) > 0 ? true : false;
    }

    public bool Delete(int id)
    {
        string sql = "delete from Person where Id=:Id";
        DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",id),
            };

        return _client.ExecuteSql(sql, paras) > 0 ? true : false;
    }

    public PersonModel Get(int id)
    {
        string sql = "select Id,Name,City from Person where Id=:Id";
        DmParameter[] paras = new DmParameter[] {
                new DmParameter(":Id",id),
            };

        PersonModel model = null;
        using (var reader = (DmDataReader)_client.ExecuteReader(sql, paras))
        {
            while (reader.Read())
            {
                model = new PersonModel();
                model.Id = reader.GetInt32(0);
                model.Name = reader.GetString(1);
                model.City = reader.GetString(2);
            }
        }

        return model;
    }

    public List<PersonModel> GetList()
    {
        var list = new List<PersonModel>();
        using (var reader = (DmDataReader)_client.ExecuteReader("select Id,Name,City from Person"))
        {
            while (reader.Read())
            {
                var model = new PersonModel();
                model.Id = reader.GetInt32(0);
                model.Name = reader.GetString(1);
                model.City = reader.GetString(2);
                list.Add(model);
            }
        }

        return list;
    }

}