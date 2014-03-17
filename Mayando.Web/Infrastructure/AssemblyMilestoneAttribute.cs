using System;

namespace Mayando.Web.Infrastructure
{
    /// <summary>
    /// Specifies the version milestone of the assembly (e.g. Beta 1, Release Candidate, RTM, Service Pack 1).
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AssemblyMilestoneAttribute : Attribute
    {
        /// <summary>
        /// Gets the version milestone of the assembly (e.g. Beta 1, Release Candidate, RTM, Service Pack 1).
        /// </summary>
        public string Milestone { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyMilestoneAttribute"/> class.
        /// </summary>
        /// <param name="milestone">The version milestone of the assembly (e.g. Beta 1, Release Candidate, RTM, Service Pack 1).</param>
        public AssemblyMilestoneAttribute(string milestone)
        {
            this.Milestone = milestone;
        }
    }
}