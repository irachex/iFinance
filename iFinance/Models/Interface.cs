using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iFinance.Models
{
    public interface Interface
    {
        #region User
        User getUser(string username);
        User getUser(int uid);
        void insertUser(User user);
        void updateUser(User user);
        void deleteUser(int userId);
   
        UserInfo getUserInfo(int uid);
        void updateUserInfo(UserInfo userinfo);
        void insertUserInfo(UserInfo userinfo);
        void deleteUserInfo(int userId);
        #endregion

        #region Account
        List<Account> getAccountsByUser(int uid, bool asc=true);
        List<Account> getAccountsByDate(int uid, DateTime start, DateTime end);
        Account getAccount(int id);
        void insertAccount(Account account);
        void updateAccount(Account account);
        void deleteAccount(int id);
        #endregion

        #region Trash
        void putbackTrash(int id);
        void deleteTrash(int id);
        void emptyTrash();
        #endregion

        #region Tag
        List<Tag> getTagsByUser(int uid);
        Tag getTag(int id);
        void insertTag(Tag tag);
        void updateTag(Tag tag);
        void deleteTag(int id);
        #endregion

        #region Tag_Account
        List<Account> getAccountsByTag(int tagId);
        List<Account> getAccountsByTags(List<int> tagIds);
        void insertAccountTag(int tagId, int accountId);
        void deleteAccountTag(int tagId, int accountId);
        #endregion

        #region Note
        void insertNote(Note mes);
        void deleteNote(int mesId);
        void updateNote(Note mes);
        List<Note> getNotesByUser(int uid);
        Note getNoteById(int id);
        #endregion
    }
}