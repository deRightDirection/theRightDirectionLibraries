using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Data
{
    /// <summary>
    /// interface for an entity which can be used in a repository
    /// the identifier makes every entity unique
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// unique identifier of an entity
        /// </summary>
        Guid Identifier { get; set; }
    }
}