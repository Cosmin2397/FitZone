using FitZone.Client.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.Utilities
{
    public class UserState
    {
        private static readonly UserState _instance = new UserState();
        private bool isAuthentificated;
        private string userName;
        private string role;
        private string id;
        private string jwtToken;

        private SubscriptionDto subscription;

        private UserState() { }

        public static UserState Instance
        {
            get { return _instance; }
        }

        public void SetUserState(bool state, string userName, string role, string id, string jwtToken)
        {
            this.isAuthentificated = state;
            this.userName = userName;
            this.role = role;
            this.id = id;
            this.jwtToken = jwtToken;
        }

        public void SetUserSubscription(SubscriptionDto subscriptionDto)
        {
            subscription = subscriptionDto;
        }
        public bool IsAuthentificated
        {
            get { return isAuthentificated; }
        }

        public string GetUsername
        {
            get { return userName; }
        }

        public string GetJwtToken
        {
            get { return jwtToken; }
        }

        public string GetRole
        {
            get { return role; }
        }

        public string GetId
        {
            get { return id; }
        }

        public SubscriptionDto GetSubscription
        {
            get { return subscription; }
        }
    }

}
