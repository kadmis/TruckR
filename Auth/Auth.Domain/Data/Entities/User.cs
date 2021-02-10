using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using Auth.Domain.Security.Passwords;
using Auth.Domain.Specifications.Password;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Data.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; private set; }

        public UserName Username { get; private set; }
        public UserPassword Password { get; private set; }
        public UserEmail Email { get; private set; }

        public Guid? ActivationId { get; private set; }
        public bool Active { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public DateTime? DeletedDate { get; private set; }

        public bool IsDeleted => DeletedDate.HasValue;
        public bool Inactive => !Active;

        public User(Guid id, UserName username, UserPassword password, UserEmail email)
        {
            ThrowIfEmailAndUsernameMatch(email, username);

            Id = id;
            Username = username;
            Password = password;
            Email = email;
            CreatedDate = DateTime.Now;
            ModifiedDate = CreatedDate;
            ActivationId = Guid.NewGuid();
        }
        public User(UserName username, UserPassword password, UserEmail email) : this(Guid.NewGuid(), username, password, email)
        {
        }
        private User() { }

        /// <summary>
        /// Sets user as deleted, inactive, removes username and email and resets password.
        /// </summary>
        /// <returns></returns>
        public User Delete()
        {
            ThrowIfUserDeleted();

            Active = false;
            ActivationId = null;
            Username = null;
            Password = UserPassword.Randomize();
            Email = null;

            DeletedDate = DateTime.Now;
            ModifiedDate = DeletedDate.Value;

            return this;
        }

        /// <summary>
        /// Activates user and removes activaion id.
        /// </summary>
        /// <param name="activationId"></param>
        /// <returns></returns>
        public User Activate(Guid activationId)
        {
            ThrowIfUserDeleted();
            ThrowIfActivated();
            ThrowIfActivationIdInvalid(activationId);

            Active = true;
            ActivationId = null;
            ModifiedDate = DateTime.Now;

            return this;
        }

        /// <summary>
        /// Deactivates user, sets new activation id and resets password.
        /// </summary>
        /// <returns></returns>
        public User Deactivate()
        {
            ThrowIfUserDeleted();
            ThrowIfDeactivated();

            Active = false;
            ActivationId = Guid.NewGuid();
            Password = UserPassword.Randomize();
            ModifiedDate = DateTime.Now;

            return this;
        }

        /// <summary>
        /// Resets password.
        /// </summary>
        /// <returns></returns>
        public UserPassword ResetPassword()
        {
            ThrowIfUserDeleted();

            Password = UserPassword.Randomize();
            ModifiedDate = DateTime.Now;

            return Password;
        }

        /// <summary>
        /// Changes username to new given username that has to be different than the previous one.
        /// </summary>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        public UserName ChangeUsername(UserName newUsername)
        {
            ThrowIfUserDeleted();
            ThrowIfEmailAndUsernameMatch(Email, newUsername);
            ThrowIfNewUsernameIsTheSame(newUsername);

            ModifiedDate = DateTime.Now;
            return Username = newUsername;
        }

        /// <summary>
        /// Changes email to new given email that has to be different than the previous one.
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public UserEmail ChangeEmail(UserEmail newEmail)
        {
            ThrowIfUserDeleted();
            ThrowIfEmailAndUsernameMatch(newEmail, Username);
            ThrowIfNewEmailIsTheSame(newEmail);

            ModifiedDate = DateTime.Now;
            return Email = newEmail;
        }

        private void ThrowIfEmailAndUsernameMatch(UserEmail email, UserName username)
        {
            if (email.Email.ToUpper().Equals(username.Username.ToUpper()))
            {
                throw new UsernameEmailMatchException();
            }
        }

        private void ThrowIfUserDeleted()
        {
            if (IsDeleted)
            {
                throw new UserDeletedException();
            }
        }

        private void ThrowIfActivated()
        {
            if(Active)
            {
                throw new UserActivatedException();
            }
        }

        private void ThrowIfDeactivated()
        {
            if(Inactive)
            {
                throw new UserDeactivatedException();
            }
        }

        private void ThrowIfActivationIdInvalid(Guid id)
        {
            if(!ActivationId.Equals(id))
            {
                throw new InvalidActivationIdException();
            }
        }

        private void ThrowIfNewUsernameIsTheSame(UserName username)
        {
            if (Username == username)
            {
                throw new NewUsernameSameAsOldException();
            }
        }

        private void ThrowIfNewEmailIsTheSame(UserEmail email)
        {
            if (Email == email)
            {
                throw new NewEmailSameAsOldException();
            }
        }
    }
}
