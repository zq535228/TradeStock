using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ProtoBuf;

namespace org.jiechan.encrypt {

    [ProtoContract]
    public class ModelUser {

        [ProtoMember(1, IsRequired = true)]
        public string UID {
            get;
            set;
        }

        [ProtoMember(2, IsRequired = true)]
        public string UserName {
            get;
            set;
        }

        [ProtoMember(3, IsRequired = true)]
        public string PassWord {
            get;
            set;
        }

        [ProtoMember(4)]
        public string FromIP {
            get;
            set;
        }

        [ProtoMember(5)]
        public string CpuID {
            get;
            set;
        }

        [ProtoMember(6)]
        public int GroupID {
            get;
            set;
        }
        [ProtoMember(7)]
        public string GroupStr {
            get;
            set;
        }
        [ProtoMember(8)]
        public string CallBackMessage {
            get;
            set;
        }

        [ProtoMember(9)]
        public bool IsSuccess {
            get;
            set;
        }
        [ProtoMember(10)]
        public bool IsVIP {
            get;
            set;
        }
        [ProtoMember(11)]
        public DateTime LoginTime {
            get;
            set;
        }
        [ProtoMember(12)]
        public DateTime ExpTime {
            get;
            set;
        }

        [ProtoMember(13)]
        public int UserMoney {
            get;
            set;
        }

        [ProtoMember(14)]
        public string formHash {
            get;
            set;
        }

        [ProtoMember(16)]
        public string HashKey { get; set; }
    }
}
