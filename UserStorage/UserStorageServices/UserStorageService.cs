using System;
using System.Collections.Generic;

namespace UserStorageServices
{
    using System.Linq;

    /// <summary>
    /// Represents a service that stores a set of <see cref="User"/>s and allows to search through them.
    /// </summary>
    public class UserStorageService
    {
        /// <summary>
        /// User storage
        /// </summary>
        private HashSet<User> _storage = new HashSet<User>();

        /// <summary>
        /// Gets the number of elements contained in the storage.
        /// </summary>
        /// <returns>An amount of users in the storage.</returns>
        public int Count => this._storage.Count;

        private bool IsLoggingEnabled;

        /// <summary>
        /// Adds a new <see cref="User"/> to the storage.
        /// </summary>
        /// <param name="user">A new <see cref="User"/> that will be added to the storage.</param>
        public void Add(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Add() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.FirstName))
            {
                throw new ArgumentException("FirstName is null or empty or whitespace", nameof(user));
            }

            if (string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new ArgumentException("LastName is null or empty or whitespace", nameof(user));
            }

            if (user.Age <= 0 || user.Age > 140)
            {
                throw new ArgumentException("Age is incorrect", nameof(user));
            }

            user.Id = Guid.NewGuid();
            this._storage.Add(user);
        }

        /// <summary>
        /// Removes an existed <see cref="User"/> from the storage.
        /// </summary>
        public void Remove(User user)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Remove() method is called.");
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
                
            var storageContainsUser = _storage.Contains(user);
            
            if (!storageContainsUser)
            { 
                throw new InvalidOperationException("There is no such user in the storage");
            }
            
            _storage.Remove(user);
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public User Search(Guid id)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Search() method is called.");
            }
            
            return _storage.First(new Func<User, bool>(a => a.Id == id));
        }

        /// <summary>
        /// Searches through the storage for a <see cref="User"/> that matches specified criteria.
        /// </summary>
        public User Search(string lastName)
        {
            if (IsLoggingEnabled)
            {
                Console.WriteLine("Search() method is called.");
            }

            return _storage.First(new Func<User, bool>(a => a.LastName == lastName));
        }
    }
}
