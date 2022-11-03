import React, {useState,useEffect} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { useParams } from "react-router-dom";
import '../App.css'

function Patrocinadores() {
  const params = useParams();

  const getPtrsUrl = "https://localhost:7164/api/patrocinador";
  const getMayorPtrsUrl = `https://localhost:7164/api/patrocinador/Mayor_Monto/${params.id}`;

  const [data, setData] = useState([]);

  const GetPatrocinadores = async () => {
    await axios.get(getPtrsUrl)
    .then(response => {
      setData(response.data);
      console.log(response.data);
    }).catch(error => {
      console.log(error);
    })
  }

  const GetMPatrocinadores = async () => {
    await axios.get(getMayorPtrsUrl)
    .then(response => {
      setData(response.data);
      console.log(response.data);
    }).catch(error => {
      console.log(error);
    })
  }

  useEffect(()=> {
    GetPatrocinadores();
  },[])

  return (
    
    <div className='wrapper' >
          <table className='table table-bordered'>
            <thead>
              <tr>
                <th>Nombre</th>
                <th>Monto</th>
                <th>Origen de dinero</th>
                <th>Heroe</th>
              </tr>
            </thead>

            <tbody>
              {data.map(p=>(
              <tr key={p.id}>
                <td>{p.nombre}</td>
                <td>{p.monto}</td>
                <td>{p.origenDinero}</td> 
                <td>{p.heroe.idPersonaNavigation.apodo}</td> 
              </tr>
              ))}
            </tbody>
          </table>
    </div> 
  );
}

export default Patrocinadores;