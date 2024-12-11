using parc.Dto;
using parc.Models;

namespace parc.Services;

public static class ParcService
{
    public static Parc Add(ParcDto parcDto)
    {
        return new Parc();
    }

    public static List<Parc> GetAll()
    {
        return new List<Parc>();
    }

    public static Parc GetById(int id)
    {
        return new Parc();
    }

    public static Parc Update(int id, ParcDto parcDto)
    {
        return new Parc();
    }

    public static bool Delete(int id)
    {
        return true;
    }
}