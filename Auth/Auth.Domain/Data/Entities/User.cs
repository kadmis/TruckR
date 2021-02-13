using Auth.Domain.Data.ValueObjects;
using Auth.Domain.Exceptions.UserExceptions;
using System;

namespace Auth.Domain.Data.Entities
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; }

        public Username Username { get; private set; }
        public Password Password { get; private set; }
        public Email Email { get; private set; }

        public Guid? ActivationId { get; private set; }
        public bool Active { get; private set; }

        public Guid? PasswordResetToken { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public DateTime? DeletedDate { get; private set; }

        public bool IsDeleted => DeletedDate.HasValue;
        public bool Inactive => !Active;

        public User(Guid id, Username username, Password password, Email email)
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
        public User(Username username, Password password, Email email) : this(Guid.NewGuid(), username, password, email)
        {
        }
        private User() { }

        /// <summary>
        /// Sets user as deleted, inactive, removes username, removes email and resets password.
        /// </summary>
        /// <returns></returns>
        public User Delete()
        {
            ThrowIfUserDeleted();

            Active = false;
            ActivationId = null;
            Username = null;
            Password = Password.Randomize();
            Email = null;
            DeletedDate = DateTime.Now;

            return Modified();
        }

        /// <summary>
        /// Activates user and removes activaion id.
        /// </summary>
        /// <param name="activationId"></param>
        /// <returns></returns>
        public User Activate(Guid activationId)
        {
            ThrowIfUserDeleted();
            ThrowIfActive();
            ThrowIfActivationIdInvalid(activationId);

            Active = true;
            ActivationId = null;

            return Modified();
        }

        /// <summary>
        /// Deactivates user, sets new activation id and resets password.
        /// </summary>
        /// <returns></returns>
        public User Deactivate()
        {
            ThrowIfUserDeleted();
            ThrowIfInactive();

            Active = false;
            ActivationId = Guid.NewGuid();
            Password = Password.Randomize();

            return Modified();
        }

        /// <summary>
        /// Resets password.
        /// </summary>
        /// <returns></returns>
        public User ResetPassword()
        {
            ThrowIfUserDeleted();
            ThrowIfInactive();

            Password = Password.Randomize();
            PasswordResetToken = Guid.NewGuid();

            return Modified();
        }

        /// <summary>
        /// Sets password to the new, given password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public User SetPassword(Password password, Guid resetToken)
        {
            ThrowIfUserDeleted();
            ThrowIfInactive();
            ThrowIfPasswordResetTokenIsInvalid(resetToken);

            Password = password;
            PasswordResetToken = null;

            return Modified();
        }

        /// <summary>
        /// Changes username to new given username that has to be different than the previous one.
        /// </summary>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        public User ChangeUsername(Username newUsername)
        {
            ThrowIfUserDeleted();
            ThrowIfEmailAndUsernameMatch(Email, newUsername);
            ThrowIfNewUsernameIsTheSame(newUsername);

            Username = newUsername;

            return Modified();
        }

        /// <summary>
        /// Changes email to new given email that has to be different than the previous one.
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public User ChangeEmail(Email newEmail)
        {
            ThrowIfUserDeleted();
            ThrowIfEmailAndUsernameMatch(newEmail, Username);
            ThrowIfNewEmailIsTheSame(newEmail);

            Email = newEmail;
            
            return Modified();
        }

        private User Modified()
        {
            ModifiedDate = DateTime.Now;
            return this;
        }

        private void ThrowIfEmailAndUsernameMatch(Email email, Username username)
        {
            if (email.Value.ToUpper().Equals(username.Value.ToUpper()))
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

        private void ThrowIfActive()
        {
            if(Active)
            {
                throw new UserActiveException();
            }
        }

        private void ThrowIfInactive()
        {
            if(Inactive)
            {
                throw new UserInactiveException();
            }
        }

        private void ThrowIfActivationIdInvalid(Guid id)
        {
            if(!ActivationId.Equals(id))
            {
                throw new InvalidActivationIdException();
            }
        }

        private void ThrowIfNewUsernameIsTheSame(Username username)
        {
            if (Username == username)
            {
                throw new NewUsernameSameAsOldException();
            }
        }

        private void ThrowIfNewEmailIsTheSame(Email email)
        {
            if (Email == email)
            {
                throw new NewEmailSameAsOldException();
            }
        }

        private void ThrowIfPasswordResetTokenIsInvalid(Guid resetToken)
        {
            if(PasswordResetToken == null || resetToken != PasswordResetToken)
            {
                throw new InvalidPasswordResetTokenException();
            }
        }
    }
}
