namespace Trabalhinho.MusicasRotas;

public class Artista{

    public Guid Id {
        get;init; //id nao alterar (init)
    }
    public string Nome {get;set;}
    public string Album {get;set;}
    public string Musica {get;set;}

    public Artista(String nome, String album, String musica){
        Nome = nome;
        Album = album;
        Musica = musica;
        Id = Guid.NewGuid();
    }

}