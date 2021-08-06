namespace ZAP.Commons
{
    public class InstanceLaunchOptions
    {
        public InstanceLaunchOptions()
        {

        }

        public InstanceLaunchOptions(string assignedId)
        {
            AssignedId = assignedId;
        }

        public string AssignedId { get; set; }
        public string Profile { get; set; }
        public string LaunchArgmuent { get; set; }
    }
}
