using BookStore.Model.Models;
using System.Linq;

namespace BookStore.Model.Dao
{
    public class CredentialDao
    {
        private BookStoreDbContext db = null;

        public CredentialDao()
        {
            db = new BookStoreDbContext();
        }

        public void Insert(string roleID, int userGrID)
        {
            var credential = new Credential();
            credential.UserGroupID = userGrID;
            credential.RoleID = roleID;
            db.Credentials.Add(credential);
            db.SaveChanges();
        }

        public void Create(UserGroup userGroup, string Role)
        {
            if (!string.IsNullOrEmpty(Role))
            {
                foreach (var role in Role.Split(','))
                {
                    this.Insert(role, userGroup.ID);
                }
            }
        }

        public void Update(UserGroup userGroup, string Role)
        {
            this.RemoveAllCredential(userGroup.ID);
            if (!string.IsNullOrEmpty(Role))
            {
                foreach (var role in Role.Split(','))
                {
                    this.Insert(role, userGroup.ID);
                }
            }
        }

        public void RemoveAllCredential(int id)
        {
            db.Credentials.RemoveRange(db.Credentials.Where(x => x.UserGroupID == id));
            db.SaveChanges();
        }
    }
}