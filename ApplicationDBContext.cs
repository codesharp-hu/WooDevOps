
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<HostingEnvironment> HostingEnvironments { get; set; } = null!;
    public DbSet<Pipeline> Pipelines { get; set; } = null!;
    public DbSet<PipelineState> PipelineStates { get; set; } = null!;
    public DbSet<PipelineDescriptor> PipelineDescriptors { get; set; } = null!;
    public DbSet<JobState> JobStates { get; set; } = null!;
    public DbSet<JobDescriptor> JobDescriptors { get; set; } = null!;
    public DbSet<JobParameter> JobParameters { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasData(
            new Project
            {
                Id = 1,
                Name = "Sample Project 1",
                ProductionId = 1,
                StagingId = 2,
            },
            new Project
            {
                Id = 2,
                Name = "Sample Project 2",
                ProductionId = 3,
                StagingId = 4,
            }
        );
        modelBuilder.Entity<PipelineDescriptor>().HasData(
            new PipelineDescriptor
            {
                Id = 1,
                Name = "Sample Pipeline 1",
                ProjectId = 1,
            },
            new PipelineDescriptor
            {
                Id = 2,
                Name = "Sample Pipeline 2",
                ProjectId = 2,
            }
        );
        modelBuilder.Entity<JobDescriptor>().HasData(
            new JobDescriptor
            {
                Id = 1,
                PipelineDescriptorId = 1,
                Name = "Sample Job 1",
                Command = "echo \"Hello World 1 \" && sleep 1 && echo \"Hello World 2 $JobParameter\" && sleep 1 && echo \"Hello World 3 $JobParameter\" && sleep 1 && echo \"Hello World 4 $JobParameter2\" && sleep 1 && echo \"Hello World 5\"",
            },
            new JobDescriptor
            {
                Id = 2,
                PipelineDescriptorId = 2,
                Name = "Sample Job 2",
                Command = "echo \"Hello World 1 \" && sleep 1 && echo \"Hello World 2 $JobParameter\" && sleep 1 && echo \"Hello World 3 $JobParameter\" && sleep 1 && echo \"Hello World 4 $JobParameter2\" && sleep 1 && echo \"Hello World 5\"",
            }
        );
        modelBuilder.Entity<JobParameter>().HasData(
            new JobParameter
            {
                Id = 1,
                JobDescriptorId = 1,
                Name = "JobParameter",
                Value = "JobParameterValue"
            },
            new JobParameter
            {
                Id = 2,
                JobDescriptorId = 1,
                Name = "JobParameter2",
                Value = "JobParameterValue2"
            },
            new JobParameter
            {
                Id = 3,
                JobDescriptorId = 2,
                Name = "JobParameter",
                Value = "JobParameterValue"
            },
            new JobParameter
            {
                Id = 4,
                JobDescriptorId = 2,
                Name = "JobParameter2",
                Value = "JobParameterValue2"
            }
        );
        modelBuilder.Entity<PipelineState>().HasData(
            new PipelineState
            {
                Id = 1,
                PipelineDescriptorId = 1,
                State = State.Succeeded
            },
            new PipelineState
            {
                Id = 2,
                PipelineDescriptorId = 2,
                State = State.Failed
            }
        );
        modelBuilder.Entity<JobState>().HasData(
            new JobState
            {
                Id = 1,
                PipelineStateId = 1,
                State = State.Succeeded,
                Messages = new List<string>
                {
                    "Message 1",
                    "Message 2"
                },
            },
            new JobState
            {
                Id = 2,
                PipelineStateId = 1,
                State = State.Failed,
                Messages = new List<string>
                {
                    "Message 1",
                    "Message 2"
                },
            }
        );
        modelBuilder.Entity<HostingEnvironment>().HasData(
            new HostingEnvironment
            {
                Id = 1,
                ApiEndpoint = new Uri("https://api.example.com"),
                SshEndpoint = "ssh.example.com",
            },
            new HostingEnvironment
            {
                Id = 2,
                ApiEndpoint = new Uri("https://api.example.com"),
                SshEndpoint = "ssh.example.com",
            },
            new HostingEnvironment
            {
                Id = 3,
                ApiEndpoint = new Uri("https://api.example.com"),
                SshEndpoint = "ssh.example.com",
            },
            new HostingEnvironment
            {
                Id = 4,
                ApiEndpoint = new Uri("https://api.example.com"),
                SshEndpoint = "ssh.example.com",
            }
        );
    }
}