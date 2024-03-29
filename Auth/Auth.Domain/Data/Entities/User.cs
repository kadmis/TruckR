﻿using Auth.Domain.Data.Rules;
using Auth.Domain.Data.ValueObjects;
using BuildingBlocks.Domain;
using System;

namespace Auth.Domain.Data.Entities
{
    public class User : Entity, IAggregateRoot
    {
        public Guid Id { get; }

        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public Username Username { get; private set; }
        public Password Password { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public UserRole Role { get; private set; }

        public Guid? ActivationId { get; private set; }
        public bool Active { get; private set; }

        public Guid? PasswordResetToken { get; private set; }

        public DateTime CreatedDate { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public DateTime? DeletedDate { get; private set; }

        private User(Guid id, string firstname, string lastname, Username username, Password password, Email email, PhoneNumber phoneNumber, UserRole role)
        {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            CreatedDate = Clock.Now;
            ModifiedDate = CreatedDate;
            ActivationId = Guid.NewGuid();
        }
        private User() { }

        /// <summary>
        /// Creates a new instance of a user entity with a given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User Create(Guid id, string firstname, string lastname, string username, string password, string email, string phoneNumber, UserRole role)
        {
            return new User(
                id, 
                firstname, 
                lastname, 
                new Username(username), 
                new Password(password), 
                new Email(email), 
                new PhoneNumber(phoneNumber), 
                role);
        }

        /// <summary>
        /// Creates a new instance of a user entity with automatically generated Id.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User Create(string firstname, string lastname, string username, string password, string email, string phoneNumber, UserRole role)
        {
            return new User(
                Guid.NewGuid(), 
                firstname, 
                lastname, 
                new Username(username), 
                new Password(password), 
                new Email(email), 
                new PhoneNumber(phoneNumber), 
                role);
        }

        /// <summary>
        /// Sets user as deleted, inactive, removes username, removes email and resets password.
        /// </summary>
        /// <returns></returns>
        public User Delete()
        {
            CheckRule(new UserCannotBeInDeletedState(this));

            Active = false;
            ActivationId = null;
            Username = null;
            Password = Password.Randomize();
            Email = null;
            DeletedDate = Clock.Now;

            return Modified();
        }

        /// <summary>
        /// Activates user and removes activation id.
        /// </summary>
        /// <param name="activationId"></param>
        /// <returns></returns>
        public User Activate(Guid activationId)
        {
            CheckRule(new UserCannotBeInDeletedState(this));
            CheckRule(new UserCannotBeInActiveState(this));
            CheckRule(new ActivationTokenHasToBeValid(activationId, this));

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
            CheckRule(new UserCannotBeInDeletedState(this));
            CheckRule(new UserCannotBeInInactiveState(this));

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
            CheckRule(new UserCannotBeInDeletedState(this));
            CheckRule(new UserCannotBeInInactiveState(this));

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
            CheckRule(new UserCannotBeInDeletedState(this));
            CheckRule(new UserCannotBeInInactiveState(this));
            CheckRule(new PasswordResetTokenHasToBeValid(resetToken, this));

            Password = password;
            PasswordResetToken = null;

            return Modified();
        }

        /// <summary>
        /// Changes phone number to a newly given.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public User ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            CheckRule(new UserCannotBeInDeletedState(this));
            CheckRule(new UserCannotBeInInactiveState(this));

            PhoneNumber = phoneNumber;

            return Modified();
        }

        private User Modified()
        {
            ModifiedDate = Clock.Now;
            return this;
        }
    }
}
