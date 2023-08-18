namespace BashScriptRunner.Service;

public class ProjectService
{
    public List<Project> ListProjects()
    {
        var projects = new List<Project> 
        {
            new Project 
            {
                Name = "Project 1",
                Production = new HostingEnvironment(),
                Staging = new HostingEnvironment(),
                Pipelines = new List<PipelineDescriptor>
                {
                    new PipelineDescriptor
                    {
                        Name = "Pipeline 1",
                        Jobs = new List<JobDescriptor>
                        {
                            new JobDescriptor
                            {
                                Name = "Job 1",
                                Command = "echo \"Hello World 1 \" && sleep 1 && echo \"Hello World 2 $1\" && sleep 1 && echo \"Hello World 3 $1\" && sleep 1 && echo \"Hello World 4\" && sleep 1 && echo \"Hello World 5\"",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            },
                            new JobDescriptor
                            {
                                Name = "Job 2",
                                Command = "echo \"Hello World 1 $1\" && sleep 1 && echo \"Hello World 2\" && sleep 1 && echo \"Hello World 3 $1\" && sleep 1 && echo \"Hello World 4\" && sleep 1 && echo \"Hello World 5\"",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            },
                            new JobDescriptor
                            {
                                Name = "Job 3",
                                Command = "echo \"Hello World 1 $1\" && sleep 1 && echo \"Hello World 2\" && sleep 1 && echo \"Hello World 3\" && sleep 1 && echo \"Hello World 4 $1\" && sleep 1 && echo \"Hello World 5\"",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            }
                        },
                        Runs = new List<PipelineState>{
                            new PipelineState
                            {
                                State = State.Failed,
                                JobStates = new List<JobState>
                                {
                                    new JobState
                                    {
                                        State = State.Failed,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    },
                                    new JobState
                                    {
                                        State = State.Failed,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    }
                                }
                            },
                            new PipelineState
                            {
                                State = State.Succeeded,
                                JobStates = new List<JobState>
                                {
                                    new JobState
                                    {
                                        State = State.Succeeded,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    },
                                    new JobState
                                    {
                                        State = State.Succeeded,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    }
                                }
                            }
                        }
                    },
                    new PipelineDescriptor
                    {
                        Name = "Pipeline 2",
                        Jobs = new List<JobDescriptor>
                        {
                            new JobDescriptor
                            {
                                Name = "Job 1",
                                Command = "echo \"Hello World 1 $1\" && sleep 1 && echo \"Hello World 2\" && sleep 1 && echo \"Hello World 3\" && sleep 1 && echo \"Hello World 4 $1\" && sleep 1 && echo \"Hello World 5\"",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            },
                            new JobDescriptor
                            {
                                Name = "Job 2",
                                Command = "echo \"Hello World 1 $1\" && sleep 1 && echo \"Hello World 2 $1\" && sleep 1 && echo \"Hello World 3\" && sleep 1 && echo \"Hello World 4\" && sleep 1 && echo \"Hello World 5\"",
                                Parameters = new List<JobParameter>
                                {
                                    new JobParameter
                                    {
                                        Name = "JobParameter",
                                        Value = "JobParameterValue"
                                    }
                                }
                            }
                        },
                        Runs = new List<PipelineState>{
                            new PipelineState
                            {
                                State = State.Failed,
                                JobStates = new List<JobState>
                                {
                                    new JobState
                                    {
                                        State = State.Failed,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    },
                                    new JobState
                                    {
                                        State = State.Failed,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    }
                                }
                            },
                            new PipelineState
                            {
                                State = State.Succeeded,
                                JobStates = new List<JobState>
                                {
                                    new JobState
                                    {
                                        State = State.Succeeded,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    },
                                    new JobState
                                    {
                                        State = State.Succeeded,
                                        Messages = new List<string>
                                        {
                                            "Message 1",
                                            "Message 2"
                                        },
                                        Date = DateTime.Now
                                    }
                                }
                            }
                        }
                    },
                }
            }
        };
        return projects;
    }
}