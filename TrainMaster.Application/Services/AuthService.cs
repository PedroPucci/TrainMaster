using Serilog;
using TrainMaser.Infrastracture.Repository.Interfaces;
using TrainMaser.Infrastracture.Repository.Security.Cryptography;
using TrainMaser.Infrastracture.Security.Token.Access;
using TrainMaster.Application.ExtensionError;
using TrainMaster.Shared.Logging;

namespace TrainMaster.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly BCryptoAlgorithm _crypto;

        public AuthService(IUserRepository userRepository, TokenService tokenService, BCryptoAlgorithm crypto)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _crypto = crypto;
        }

        public async Task<Result<string>> Login(string cpf, string password)
        {
            try
            {
                var user = await _userRepository.GetByCpf(cpf);

                if (user == null)
                    Log.Error("CPF or password is incorrect.");

                if (!_crypto.VerifyPassword(password, user.Password))
                    Log.Error("CPF or password is incorrect.");

                var token = _tokenService.GenerateToken(user.Id.ToString(), user.Email);
                Log.Information($"User {user.Email} logged in successfully."); 

                return Result<string>.Ok(token);
            }
            catch (Exception ex)
            {
                Log.Error(LogMessages.AddingUserError(ex));
                return Result<string>.Error("An unexpected error occurred during login.");
            }
        }

        public async Task<Result<string>> Logout(string token)
        {
            //// Obter a data de expiração do token
            //var handler = new JwtSecurityTokenHandler();
            //var jwtToken = handler.ReadJwtToken(token);
            //var expiration = jwtToken.ValidTo;

            //// Salvar o token na lista de tokens revogados
            //var revokedToken = new RevokedToken
            //{
            //    Token = token,
            //    Expiration = expiration
            //};

            //await _repositoryUoW.RevokedTokenRepository.Add(revokedToken);
            //await _repositoryUoW.SaveAsync();

            return Result<string>.Ok("User logged out successfully.");
        }
    }
}
