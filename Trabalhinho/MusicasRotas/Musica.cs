namespace Trabalhinho.MusicasRotas;

public static class Musica{
    public static void AddMusicas(this WebApplication app ){

        //Inserir
        var artistasInserir = app.MapGroup("api/artistas");

        artistasInserir.MapPost("",(Artista artista, AppDbContext context)=>{

            var novoArtista = new Artista(artista.Nome,artista.Album,artista.Musica);

            context.Artistas.Add(novoArtista);
            context.SaveChanges();

            return Results.Ok(novoArtista);

        });
        
        //Retorna as infos 
        
        app.MapGet("api/mostrar", (AppDbContext context)=>{
            var mostrarArtista = context.Artistas;

            return mostrarArtista;
        });

        //Atualizar as infos

        app.MapPut("api/atualizar",(AppDbContext context, Artista atualizaArtista)=>{

            var artistas = context.Artistas.Find(atualizaArtista.Id);
            if(artistas==null){
                return Results.NotFound();
            }
            // Atualiza com os novos valores
            artistas.Nome = atualizaArtista.Nome;
            artistas.Album = atualizaArtista.Album;
            artistas.Musica = atualizaArtista.Nome;

            context.SaveChanges();

            return Results.Ok(artistas);
        
        });

        //Deletar

        app.MapDelete("api/delete",(AppDbContext context, Guid id)=>{

            var encontrar = context.Artistas.Find(id);

            if(encontrar==null){
                return Results.NotFound();
            }
            context.Artistas.Remove(encontrar);
            context.SaveChanges();
            return Results.Ok(encontrar);
   
 });


    }
}