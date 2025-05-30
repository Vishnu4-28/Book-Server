using E_commerce.Server.Model.DTO;

public static class BookReqValidator
{
    public static Dictionary<string, string> Validate(BookReq book)
    {
        var errors = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(book.Title))
            errors["Title"] = "Title is required.";

        if (string.IsNullOrWhiteSpace(book.Author))
            errors["Author"] = "Author is required.";

        if (book.ISBN <= 0)
            errors["ISBN"] = "ISBN must be a positive number.";

        if (book.Quantity <= 0)
            errors["Quantity"] = "Quantity cannot be negative.";

        return errors;
    }

    public static Dictionary<string, string> validateUser(UserReq req)
    {
        var errors = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(req.First_Name))
            errors["First_Name"] = "First Name is required.";

        if (string.IsNullOrWhiteSpace(req.Last_Name))
            errors["Last_Name"] = "Last Name is required.";

        if (req.Date_Of_Birth == default)
            errors["Date_Of_Birth"] = "Date of Birth is required.";

        if (string.IsNullOrWhiteSpace(req.Email) || !req.Email.Contains("@"))
            errors["Email"] = "Valid Email is required.";

        if (string.IsNullOrWhiteSpace(req.Password) || req.Password.Length < 6)
            errors["Password"] = "Password must be at least 6 characters long.";

        if (string.IsNullOrWhiteSpace(req.Phone_Number) || req.Phone_Number.Length < 10)
            errors["Phone_Number"] = "Phone Number must be at least 10 characters long.";

        return errors;

    }






}
