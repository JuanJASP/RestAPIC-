using Microsoft.EntityFrameworkCore;

namespace Trabalhinho.MusicasRotas;

public static class Musica{
    public static void AddMusicas(this WebApplication app ){

        //Inserir
     
        app.MapPost("api/artistas",(Artista artista, AppDbContext context)=>{

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

        app.MapGet("ap/mostrar/{id}",(AppDbContext context, Guid id)=>{
            var artistaId = context.Artistas.Find(id);
            if (artistaId == null) return Results.NotFound();

            return Results.Ok(artistaId);

        });
        app.MapGet("api/mostrar/{id}/musicas",(AppDbContext context , Guid id)=>{
            var musicasArtista = context.Artistas.Include(artista => artista.Musica).FirstOrDefault(artista => artista.Id == id);
            if (musicasArtista == null) return Results.NotFound();

            return Results.Ok(musicasArtista);
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

        app.MapPatch("ap/atualizar/musica/{id}",(AppDbContext context,Artista especificoArtista, Guid id)=>{

            var atualizar = context.Artistas.Find(id);
            if(atualizar==null){
                return Results.NotFound();
            }

            if(especificoArtista.Musica != null){
                atualizar.Musica = especificoArtista.Musica;
            }
            context.SaveChanges();
            return Results.Ok(atualizar);    
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