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
}
