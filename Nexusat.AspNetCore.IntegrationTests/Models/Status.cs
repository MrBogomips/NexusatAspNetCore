using System;
namespace Nexusat.AspNetCore.IntegrationTests.Models
{
    public class Status: IEquatable<Status>
    {
        public int HttpCode { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string UserDescription { get; set; }

		public override bool Equals(object obj)
		{
            return Equals(obj as Status);
		}

        public bool Equals(Status that) {
            return
                that != null &&
                this.HttpCode == that.HttpCode &&
                this.Code == that.Code &&
                this.Description == that.Description &&
                this.UserDescription == that.UserDescription;
        }

		public override int GetHashCode()
		{
            throw new NotImplementedException();
		}
	}
}
