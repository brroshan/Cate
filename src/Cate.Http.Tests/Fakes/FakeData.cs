using System;

namespace Cate.Tests.Fakes
{
    public class FakeData : IEquatable<FakeData>
    {
        public int Id { get; set; }
        public string Data { get; set; }

        public bool Equals(FakeData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Data, other.Data);
        }
    }
}