namespace TrainMaster.Shared.Logging
{
    public static class LogMessages
    {
        //User
        public static string InvalidUserInputs() => "Message: Invalid inputs to User.";
        public static string NullOrEmptyUserEmail() => "Message: The Email field is null, empty, or whitespace.";
        public static string UpdatingErrorUser(Exception ex) => $"Message: Error updating User: {ex.Message}";
        public static string UpdatingSuccessUser() => "Message: Successfully updated User.";
        public static string UserNotFound(string action) => $"Message: User not found for {action} action.";
        public static string AddingUserError(Exception ex) => $"Message: Error adding a new User: {ex.Message}";
        public static string AddingUserSuccess() => "Message: Successfully added a new User.";
        public static string DeleteUserError(Exception ex) => $"Message: Error to delete a User: {ex.Message}";
        public static string DeleteUserSuccess() => "Message: Delete with success User.";
        public static string GetAllUserError(Exception ex) => $"Message: Error to loading the list User: {ex.Message}";
        public static string GetAllUserSuccess() => "Message: GetAll with success User.";

        //Pessoal Profile
        public static string InvalidPessoalProfileInputs() => "Message: Invalid inputs to Pessoal Profile.";
        public static string UpdatingErrorPessoalProfile(Exception ex) => $"Message: Error updating Pessoal Profile: {ex.Message}";
        public static string UpdatingSuccessPessoalProfile() => "Message: Successfully updated Pessoal Profile.";
        public static string AddingPessoalProfileError(Exception ex) => $"Message: Error adding a new Pessoal Profile: {ex.Message}";
        public static string AddingPessoalProfileSuccess() => "Message: Successfully added a new Pessoal Profile.";
        public static string DeletePessoalProfileError(Exception ex) => $"Message: Error to delete a Pessoal Profile: {ex.Message}";
        public static string DeletePessoalProfileSuccess() => "Message: Delete with success Pessoal Profile.";
        public static string GetAllPessoalProfileError(Exception ex) => $"Message: Error to loading the list Pessoal Profile: {ex.Message}";
        public static string GetAllPessoalProfileSuccess() => "Message: GetAll with success Pessoal Profile.";

        //Address
        public static string InvalidAddressInputs() => "Message: Invalid inputs to Address.";
        public static string AddingAddressSuccess() => "Message: Successfully added a new Address.";
        public static string AddingAddressError(Exception ex) => $"Message: Error adding a new Address: {ex.Message}";
        public static string DeleteAddressError(Exception ex) => $"Message: Error to delete a Address: {ex.Message}";
        public static string DeleteAddressSuccess() => "Message: Delete with success Address.";
        public static string GetAllAddressError(Exception ex) => $"Message: Error to loading the list Address: {ex.Message}";
        public static string GetAllAddressSuccess() => "Message: GetAll with success Address.";
        public static string UpdatingErrorAddress(Exception ex) => $"Message: Error updating Address: {ex.Message}";
        public static string UpdatingSuccessAddress() => "Message: Successfully updated Address.";

        //Education Level
        public static string InvalidEducationLevelInputs() => "Message: Invalid inputs to Education Level.";
        public static string AddingEducationLevelSuccess() => "Message: Successfully added a new Education Level.";
        public static string AddingEducationLevelError(Exception ex) => $"Message: Error adding a new Education Level: {ex.Message}";
        public static string DeleteEducationLevelError(Exception ex) => $"Message: Error to delete a Education Level: {ex.Message}";
        public static string DeleteEducationLevelSuccess() => "Message: Delete with success Education Level.";
        public static string GetAllEducationLevelError(Exception ex) => $"Message: Error to loading the list Education Level: {ex.Message}";
        public static string GetAllEducationLevelSuccess() => "Message: GetAll with success Education Level.";
        public static string UpdatingErrorEducationLevel(Exception ex) => $"Message: Error updating Education Level: {ex.Message}";
        public static string UpdatingSuccessEducationLevel() => "Message: Successfully updated Education Level.";

        //Professional Profile
        public static string InvalidProfessionalProfileInputs() => "Message: Invalid inputs to Professional Profile.";
        public static string AddingProfessionalProfileSuccess() => "Message: Successfully added a new Professional Profile.";
        public static string AddingProfessionalProfileError(Exception ex) => $"Message: Error adding a new Professional Profile: {ex.Message}";
        public static string DeleteProfessionalProfileError(Exception ex) => $"Message: Error to delete a Professional Profile: {ex.Message}";
        public static string DeleteProfessionalProfileSuccess() => "Message: Delete with success Professional Profile.";
        public static string GetAllProfessionalProfileError(Exception ex) => $"Message: Error to loading the list Professional Profile: {ex.Message}";
        public static string GetAllProfessionalProfileSuccess() => "Message: GetAll with success Professional Profile.";
        public static string UpdatingErrorProfessionalProfile(Exception ex) => $"Message: Error updating Professional Profile: {ex.Message}";
        public static string UpdatingSuccessProfessionalProfile() => "Message: Successfully updated Professional Profile.";
    }
}