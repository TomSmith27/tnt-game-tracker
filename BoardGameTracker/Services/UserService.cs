namespace BoardGameTracker.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Database;
    using Exceptions;
    using Models;

    public interface IUserService
    {
        Player Authenticate(string username, string password);
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        Player Create(Player player, string password);
        void Update(Player player, string password = null);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private BoardGameContext db;

        public UserService(BoardGameContext db)
        {
            this.db = db;
        }

        public Player Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var player = db.Players.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (player == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, player.PasswordHash, player.PasswordSalt))
                return null;

            // authentication successful
            return player;
        }

        public IEnumerable<Player> GetAll()
        {
            return this.db.Players;
        }

        public Player GetById(int id)
        {
            return this.db.Players.Find(id);
        }

        public Player Create(Player player, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (this.db.Players.Any(x => x.Username == player.Username))
                throw new AppException("Username \"" + player.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            player.PasswordHash = passwordHash;
            player.PasswordSalt = passwordSalt;

            this.db.Players.Add(player);
            this.db.SaveChanges();

            return player;
        }

        public void Update(Player userParam, string password = null)
        {
            var player = this.db.Players.Find(userParam.Id);

            if (player == null)
                throw new AppException("Player not found");

            if (userParam.Username != player.Username && userParam.Username != null)
            {
                // username has changed so check if the new username is already taken
                if (this.db.Players.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update player properties
            player.Name = userParam.Name;
            if (!string.IsNullOrWhiteSpace(userParam.Username))
            {
                player.Username = userParam.Username;
            }
            player.Colour = userParam.Colour;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                player.PasswordHash = passwordHash;
                player.PasswordSalt = passwordSalt;
            }

            this.db.Players.Update(player);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var player = this.db.Players.Find(id);
            if (player != null)
            {
                this.db.Players.Remove(player);
                this.db.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
