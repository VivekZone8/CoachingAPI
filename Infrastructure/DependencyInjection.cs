using Api.Infrastructure.Authentication;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection("Jwt"));

        // Database
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        // HTTP Context
        //services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        // Authentication
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBatchService, BatchService>();
        services.AddScoped<IBatchScheduleService, BatchScheduleService>();
        services.AddScoped<IBatchStudentService, BatchStudentService>();
        services.AddScoped<IAdmissionService, AdmissionService>();

        // Repositories
        services.AddScoped<IBatchRepository, BatchRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IBatchStudentRepository, BatchStudentRepository>();
        services.AddScoped<IAdmissionRepository, AdmissionRepository>();

        // Seeder
        services.AddScoped<SuperAdminSeeder>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<ITeacherService, TeacherService>();

        services.AddScoped<ICoachingRepository, CoachingRepository>();
        services.AddScoped<ICoachingService, CoachingService>();

        services.AddScoped<IStudentRepository,StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}