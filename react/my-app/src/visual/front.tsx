import React, { ReactElement, ReactNode, useEffect, useState } from "react";
import "./style.css";


const Artista = (): ReactElement => {
  const [artistas, setArtistas] = useState([]);
  const [artistaName, setArtistaName] = useState("");
  const [albumName, setAlbumName] = useState(""); 
  const [musicaName, setMusicaName] = useState(""); 
  const [deletarID, setDeleteId] = useState(""); 

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    await fetch("http://localhost:4000/api/mostrar")
      .then((res) => res.json())
      .then((data) => {
        setArtistas(data);
      })
      .catch((error) => console.error("Erro ao buscar artistas:", error));
  };

  const submitData = async () => {
    const data = {
      nome: artistaName,
      album: albumName, 
      musica: musicaName, 
    };

    await fetch("http://localhost:4000/api/artistas", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((res) => res.json())
      .then(() => {
        fetchData();
        setArtistaName(""); 
      })
      .catch((error) => console.error("Erro ao adicionar artista:", error));
  };
  const deletarArtista = async (id: string) => {
    await fetch(`http://localhost:4000/api/delete/${id}`, {
      method: "DELETE",
    })
    .then(() => {
      setArtistas(artistas.filter((artista: any) => artista.id !== id));
    })
    .catch((error) => console.error("Erro ao deletar artista:", error));
  };

  const displayArtists = (artistas: any): ReactNode => {
    const result: any = [];
  
    if (artistas) {
      artistas.forEach((artista: any) => {
        result.push(
          <div key={artista.id} className="artista-card">
            <p>ID: {artista.id}</p><br />
            <p>Nome: {artista.nome}</p>
            <p>Álbum: {artista.album}</p>
            <p>Música: {artista.musica}</p><br />
          </div>
        );
      });
    }
    return result;
  };
  const FuncionarOnClick = () => {
    deletarArtista(deletarID); 
  };

  return (
    <React.Fragment>
      <h1>Músicas!</h1>
      {displayArtists(artistas)}

      <h2>Novo Artista</h2><br />
        <input className="form"
          type="text"
          placeholder="Nome do Artista"
          value={artistaName}
          onChange={(e) => setArtistaName(e.target.value)} 
        /> 
        <input className="form"
          type="text"
          placeholder="Álbum"
          value={albumName}
          onChange={(e) => setAlbumName(e.target.value)}
        />
        <input className="form"
          type="text"
          placeholder="Música"
          value={musicaName}
          onChange={(e) => setMusicaName(e.target.value)}
        />
        <button className="botao" onClick={submitData}>Adicionar Musica</button><br />
        <h2>Deletar Artista</h2>
    <input className="form"
      type="text"
      placeholder="ID do Artista a ser excluído"
      value={deletarID}
      onChange={(e) => setDeleteId(e.target.value)}
    />
    <button className="botao-excluir" onClick={FuncionarOnClick}>Excluir</button>
 
    </React.Fragment>
  );
};

export default Artista;