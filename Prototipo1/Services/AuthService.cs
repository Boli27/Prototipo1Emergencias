// Importación de clases y namespaces necesarios
using Prototipo1.Models;             // Para usar el modelo Usuario
using Prototipo1.Context;           // Para interactuar con la base de datos mediante AppDbContext
using Microsoft.EntityFrameworkCore; // Para usar funciones asíncronas de consulta (como FirstOrDefaultAsync)
using Microsoft.AspNetCore.Identity;
using Prototipo1.DTOs.Auths; // Para el hasheo de contraseñas (IPasswordHasher)

namespace Prototipo1.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        // Inyección de dependencias (el DbContext de la base de datos)
        public AuthService(AppDbContext context)
        {
            _context = context;

            // Inicialización del hasheador de contraseñas
            _passwordHasher = new PasswordHasher<Usuario>();
        }
        public async Task<Usuario?> RegistrarUsuario(RegisterDto dto)
        {
            // Verifica si ya existe un usuario con ese correo
            var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoInstitucional == dto.CorreoInstitucional);

            if (usuarioExistente != null)
                return null; // Si ya existe, retorna null
            // Si no existe, se crea un nuevo objeto Usuario
            var usuario = new Usuario
            {
                CorreoInstitucional = dto.CorreoInstitucional,
                Contraseña = "", // Requerido antes de hashearla
                EsBrigadista = dto.EsBrigadista // Cambiar el valor por defecto al registrarse a false y
                                                //  quitarlo del dto 
            };

            // Se hashea la contraseña usando el PasswordHasher
            usuario.Contraseña = _passwordHasher.HashPassword(usuario, dto.Contraseña);

            // Se guarda el usuario en la base de datos
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario; // Devuelve el usuario creado
        }
        public async Task<Usuario?> ValidarUsuario(LoginDto dto)
        {
            // Busca el usuario por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoInstitucional == dto.CorreoInstitucional);

            if (usuario == null)
                return null; // Si no lo encuentra, retorna null

            // Verifica que la contraseña ingresada coincida con la hasheada
            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, dto.Contraseña);

            // Si es válida, retorna el usuario. Si no, retorna null
            return resultado == PasswordVerificationResult.Success ? usuario : null;
        }
    }
}
