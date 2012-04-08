using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace iFinance.Models
{
    public class Provider:Interface
    {
        #region Private Members
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        void executeNonQuery(string sql)
        { 
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            SqlTransaction tran = con.BeginTransaction();
            try
            {
                cmd.Connection = con;
                cmd.Transaction = tran;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw new Exception("Transaction Error: " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getDataTable(string sql)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            reader.Close();
            con.Close();
            return dt;
        }
        public DataRow getDataRow(string sql)
        {
            DataTable dt = getDataTable(sql);
            if (dt == null || dt.Rows.Count == 0) return null;
            return dt.Rows[0];
        }
        

        #endregion

        #region Cast
        public User toUser(DataRow data)
        {
            if (data == null) return null;
            User user = new User();
            user.Id = (int)data["Id"];
            user.Password = (string)data["Password"];
            user.UserName = (string)data["UserName"];
            user.RegisterTime = (DateTime)data["RegisterTime"];
            user.IsAdmin = (bool)data["IsAdmin"];
            return user;
        }
        public UserInfo toUserInfo(DataRow data)
        {
            if (data == null) return null;
            UserInfo userinfo = new UserInfo();
            userinfo.Id = (int)data["Id"];
            userinfo.Email = (data["Email"] is DBNull)?"":(string)data["Email"];
            userinfo.Phone = (data["Phone"] is DBNull) ? "" : (string)data["Phone"];
            userinfo.Info = (data["Info"] is DBNull) ? "" : (string)data["Info"];
            userinfo.Address = (data["Address"] is DBNull) ? "" : (string)data["Address"];
            return userinfo;
        }

        public Account toAccount(DataRow data)
        {
            if (data == null) return null;
            Account account = new Account();
            account.Id = (int)data["Id"];
            account.Info = (string)data["Info"];
            account.Money = Convert.ToDouble(data["Money"]);
            account.State = (bool)data["State"];
            account.SubmitTime = (DateTime)data["SubmitTime"];
            account.Time = (DateTime)data["Time"];
            account.UserId = (int)data["UserId"];
            account.Type = (bool)data["Type"];
            return account;
        }

        public Tag toTag(DataRow data)
        {
            if (data == null) return null;
            Tag tag = new Tag();
            tag.Id = (int)data["Id"];
            tag.Name = (string)data["Name"];
            tag.UserId = (int)data["UserId"];
            tag.Count = (int)data["Count"];
            return tag;
        }

        public Note toNote(DataRow data)
        {
            if (data == null) return null;
            Note mes = new Note();
            mes.Id = (int)data["Id"];
            mes.Name = (string)data["Name"];
            mes.Content = (string)data["Content"];
            mes.SubmitTime = (DateTime)data["SubmitTime"];
            mes.UserId = (int)data["UserId"];
            return mes;
        }
        #endregion

        #region User

        public List<User> getAllUser()
        {
            string sql = "SELECT * FROM [User]";
            DataTable dt = getDataTable(sql);
            List<User> list = new List<User>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toUser(dr));
            }
            return list;
        }
        public List<UserInfo> getAllUserInfo()
        {
            string sql = "SELECT * FROM [UserInfo]";
            DataTable dt = getDataTable(sql);
            List<UserInfo> list = new List<UserInfo>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toUserInfo(dr));
            }
            return list;
        }
        public User getUser(string username)
        {
            DataRow data = getDataRow("SELECT * FROM [User] WHERE UserName='" + username + "'");
            User user = toUser(data);
            return user;
        }

        public User getUser(int uid)
        {
            DataRow data = getDataRow("SELECT * FROM [User] WHERE Id=" + uid );
            User user = toUser(data);
            return user;
        }

        public void insertUser(User user)
        {
            executeNonQuery("INSERT INTO [User] (UserName, Password, RegisterTime, IsAdmin) VALUES ('" + user.UserName + "', '" + user.Password + "', '" + user.RegisterTime + "',"+user.IsAdmin+")");
        }

        public void updateUser(User user)
        {
            executeNonQuery("UPDATE [User] SET  UserName='" + user.UserName + "', Password='" + user.Password + "' WHERE Id=" + user.Id);
        }

        public void deleteUser(int userId)
        {
            executeNonQuery("DELETE FROM [User] WHERE Id=" + userId);
        }

        public UserInfo getUserInfo(int uid)
        {
            DataRow data = getDataRow("SELECT * FROM [UserInfo] WHERE Id=" + uid);
            UserInfo user = toUserInfo(data);
            return user;
        }
        public void insertUserInfo(UserInfo userinfo)
        {
            executeNonQuery("INSERT INTO [UserInfo] (Id, Email, Address, Phone, Info) VALUES ("+userinfo.Id+ ",'" + userinfo.Email + "','" + userinfo.Address + "','" + userinfo.Phone + "','" + userinfo.Info + "')");
        }

        public void updateUserInfo(UserInfo userinfo)
        {
            executeNonQuery("UPDATE [UserInfo] SET Email='" + userinfo.Email + "', Address='" + userinfo.Address + "', Phone='" + userinfo.Phone + "', Info='" + userinfo.Info + "' WHERE Id=" + userinfo.Id);
        }
        public void deleteUserInfo(int userId)
        {
            executeNonQuery("DELETE FROM [UserInfo] WHERE Id=" + userId);
        }
 
        #endregion

        #region Account
        public List<Account> getAccountsByUser(int uid, bool asc = false)
        {
            string sql = "SELECT * FROM Account WHERE UserId=" + uid + " ORDER BY Time " + (asc ? "ASC" : "DESC");
            DataTable dt=getDataTable(sql);
            List<Account> list = new List<Account>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toAccount(dr));
            }
            return list;
        }

        public List<Account> getAccountsByDate(int uid, DateTime start, DateTime end)
        {
            string sql = "SELECT * FROM Account WHERE UserId=" + uid + " AND Time>='" + start.ToString() + "' AND Time<='" + end.ToString() + "' ORDER BY Time DESC";
            DataTable dt = getDataTable(sql);
            List<Account> list = new List<Account>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toAccount(dr));
            }
            return list;
        }

        public Account getAccount(int id)
        {
            DataRow data = getDataRow("SELECT * FROM Account WHERE Id=" + id);
            Account account = toAccount(data);
            return account;
        }

        public void insertAccount(Account account)
        {
            executeNonQuery("INSERT INTO Account (UserId, Money, Time, Info, State, SubmitTime, Type) VALUES ("
                                         + account.UserId + ", " + account.Money + ", '" + account.Time + "', '" + account.Info
                                         + "', 0, '" + account.SubmitTime + "', " + (account.Type ? "1" : "0") + ")");
        }

        public void updateAccount(Account account)
        {
            executeNonQuery("UPDATE Account SET UserId=" + account.UserId + ", Money=" + account.Money
                                         + ", Time='" + account.Time + "', Info='" + account.Info + "', Type=" + (account.Type ? "1" : "0") + " WHERE Id=" + account.Id);
        }

        public void deleteAccount(int id)
        {
            executeNonQuery("DELETE FROM Account WHERE Id=" + id);
        }
        #endregion

        #region Trash
        public void setTrash(int id)
        {
            executeNonQuery("UPDATE Account SET State=1 WHERE Id=" + id);
        }
        public void putbackTrash(int id)
        {
            executeNonQuery("UPDATE Account SET State=0 WHERE Id=" + id);
        }
        public void deleteTrash(int id)
        {
            deleteAccount(id);
        }
        public void emptyTrash()
        {
            executeNonQuery("DELETE FROM Account WHERE State=1");
        }
        #endregion

        #region Tag
        public List<Tag> getTagsByUser(int uid)
        {
            string sql = "SELECT * FROM Tag WHERE UserId=" + uid;
            DataTable dt = getDataTable(sql);
            List<Tag> list = new List<Tag>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toTag(dr));
            }
            return list;
        }

        public List<Tag> getTagListByName(string name)
        {
            string sql = "SELECT * FROM Tag WHERE Name='" + name + "'";
            DataTable dt = getDataTable(sql);
            List<Tag> list = new List<Tag>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toTag(dr));
            }
            return list;
        }

        public Tag getTagsByName(string name)
        {
            string sql = "SELECT * FROM Tag WHERE Name='" + name+"'";
            DataRow data = getDataRow(sql);
            return toTag(data);
        }

        public Tag getTag(int id)
        {
            DataRow data = getDataRow("SELECT * FROM Tag WHERE Id=" + id);
            Tag tag = toTag(data);
            return tag;
        }

        public void insertTag(Tag tag)
        {
            executeNonQuery("INSERT INTO Tag (UserId, Name, Count) VALUES (" + tag.UserId + ", '" + tag.Name + "', 0)");
        }

        public void updateTag(Tag tag)
        {
            executeNonQuery("UPDATE Tag SET UserId=" + tag.UserId + ", Name='" + tag.Name + "', Count=" + tag.Count + " WHERE Id=" + tag.Id);
        }

        public void deleteTag(int id)
        {
            executeNonQuery("DELETE FROM Tag WHERE Id=" + id);
        }
        public void mergeTag(int id, string name)
        {
            List<Tag> tlist=getTagListByName(name);
            foreach (Tag t in tlist)
            {
                if (t.Id == id) continue;
                executeNonQuery("UPDATE Account_Tag SET TagId=" + id + " WHERE TagId=" + t.Id);
                executeNonQuery("DELETE FROM Tag WHERE Id=" + t.Id);
            }
         }
        #endregion

        #region Account_Tag
        public bool getAccountTag(int accountId, int tagId)
        {
            DataRow dr = getDataRow("SELECT * FROM Account_Tag  WHERE AccountId=" + accountId + " AND TagId=" + tagId);
            if (dr == null) return false;
            return true;
        }

        public List<Account> getAccountsByTag(int tagId)
        {
            List<Account> list = new List<Account>();
            DataTable dt = getDataTable("SELECT Account.Id, Account.UserId, Account.Money, Account.Time, Account.Info, Account.State, Account.SubmitTime, Account.Type FROM Account, Account_Tag WHERE Account.Id=Account_Tag.AccountId AND TagId=" + tagId+" ORDER BY Time DESC");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toAccount(dr));
            }
            return list;
        }
        public List<Account> getAccountsByTags(List<int> tagIds)
        {
            List<Account> list = new List<Account>();
            foreach (int tagId in tagIds)
            {
                List<Account> tl = getAccountsByTag(tagId);
                foreach (Account t in tl)
                {
                    bool flag = true;
                    foreach (Account account in list)
                    {
                        if (account.Id == t.Id)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) list.Add(t);
                }
            }
            return list;
        }
        public List<Tag> getTagsByAccount(int accountId)
        {
            List<Tag> list = new List<Tag>();
            DataTable dt = getDataTable("SELECT Tag.Id, Tag.UserId, Tag.Name, Tag.Count FROM Tag, Account_Tag WHERE Tag.Id=Account_Tag.TagId AND AccountId=" + accountId);

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toTag(dr));
            }
            return list;
        }
        public void insertAccountTag(int accountId, int tagId)
        {
            executeNonQuery("INSERT INTO Account_Tag (AccountId, TagId) VALUES (" + accountId + "," + tagId + ")");
        }

        public void deleteAccountTag(int accountId, int tagId)
        {
            executeNonQuery("DELETE FROM Account_Tag WHERE AccountId=" + accountId + " AND TagId=" + tagId);
        }

        public void deleteAccountTagByTag(int tagId)
        {
            executeNonQuery("DELETE FROM Account_Tag WHERE TagId=" + tagId);
        }

        public void deleteAccountTagByAccount(int accountId)
        {
            executeNonQuery("DELETE FROM Account_Tag WHERE AccountId=" + accountId);
        }
        #endregion

        #region Note
        public List<Note> getNotesByUser(int uid)
        {
            string sql = "SELECT * FROM Note WHERE UserId=" + uid + " ORDER BY SubmitTime DESC";
            DataTable dt = getDataTable(sql);
            List<Note> list = new List<Note>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(toNote(dr));
            }
            return list;
        }
        public Note getNoteById(int id)
        {
            DataRow data = getDataRow("SELECT * FROM [Note] WHERE Id=" + id);
            Note m = toNote(data);
            return m;
        }
        public void insertNote(Note mes)
        {
            executeNonQuery("INSERT INTO Note (UserId, Name, Content, SubmitTime) VALUES ("
                                         + mes.UserId + ", '" + mes.Name + "', '" + mes.Content + "', '" + mes.SubmitTime + "')");
        }

        public void updateNote(Note mes)
        {
            executeNonQuery("UPDATE Note SET UserId=" + mes.UserId + ", Name='" + mes.Name
                                         + "', Content='" + mes.Content + "' WHERE Id=" + mes.Id);
        }

        public void deleteNote(int id)
        {
            executeNonQuery("DELETE FROM Note WHERE Id=" + id);
        }
      
        #endregion
    }
}