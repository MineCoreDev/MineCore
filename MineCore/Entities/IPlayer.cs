using System;
using System.Collections.Generic;
using System.Text;

namespace MineCore.Entities
{
    public interface IPlayer : IEntityHuman
    {
        Data.ILoginData LoginData { get; set; }
    }
}