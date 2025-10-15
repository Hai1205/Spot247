namespace Backend.Utils.Mappers;

public static class UserMapper
{
    public static UserDto MapEntityToDto(User entity)
    {
        if (entity == null) return null!;

        return new UserDto
        {
            Id = entity.UserId.ToString(),
            Username = entity.Username,
            Role = entity.Role,
        };
    }

    public static List<UserDto> MapListEntityToListDto(IEnumerable<User> entities)
    {
        return [.. entities
                .Where(u => u != null)
                .Select(MapEntityToDto)];
    }

    public static User MapDtoToEntity(UserDto dto)
    {
        if (dto == null) return null!;

        return new User
        {
            UserId = string.IsNullOrEmpty(dto.Id) ? 0 : int.Parse(dto.Id),
            Username = dto.Username ?? null!,
            Role = dto.Role ?? "staff",
        };
    }

    public static List<User> MapListDtoToListEntity(IEnumerable<UserDto> dtos)
    {
        return [.. dtos
                .Where(c => c != null)
                .Select(MapDtoToEntity)];
    }

}
