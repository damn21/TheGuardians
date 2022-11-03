import React, {useState,useEffect} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import '../App.css'
import {Button,Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';

function Villanos () {

    const getVillanosUrl = "https://localhost:7164/api/villanos";
    const postVillanosUrl = "https://localhost:7164/api/villanos/AgregarVillano";
    const [data, setData] = useState([]);
    const [data1, setData1] = useState([]);

    const [tablaUsuarios, setTablaUsuarios]= useState([]);
    const [busqueda, setBusqueda]= useState("");

    const [modalOpen, setModalOpen] = useState(false);
    const [Gestor,setGestor] =  useState({
        "origen":'',
        "persona":{
          "nombre":'',
          "apellido":'',
          "apodo":'',
          "edad": '',
          "debilidad":'',
          "habilidadPoder":''
        }
    });

    const handleChange = (i) => {
        const {name, value} = i.target;
        setGestor({
            ...Gestor,
            [name]:value
        });
        console.log(Gestor);
    }


    const GetVillanos = async () => {
      await axios.get(getVillanosUrl)
      .then(response => {
        setData(response.data);
        setTablaUsuarios(response.data);
        console.log(response.data);
      }).catch(error => {
        console.log(error);
      })
    }

    const PostVillanos = async () => {
       Gestor.persona.edad = parseInt(Gestor.persona.edad);
        await axios.post(postVillanosUrl,Gestor)
        .then(response => {
          setData1(data.concat(response.data));
          console.log(response.data);
          setModalOpen(false)
        }).catch(error => {
          console.log(error);
        })
      }

      const handleChange2=e=>{
        setBusqueda(e.target.value);
        filtrar(e.target.value);
      }
      
      const filtrar=(terminoBusqueda)=>{
        var resultadosBusqueda=tablaUsuarios.filter((elemento)=>{
          if( elemento.persona.apodo.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
          || elemento.origen.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
          || elemento.persona.debilidad.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
          )
          {
            return elemento;
          }
        });
        setData(resultadosBusqueda);
      }

    useEffect(()=> {
        GetVillanos();
      },[])

 return (

    <div className='table-responsive'>

<div className="containerInput">
        <input
        className="form-control inputBuscar"
        value={busqueda}
        placeholder="Búsqueda por Nombre, Origen o Debilidad"
        onChange={handleChange2}
      />

      <button className="btn btn-success">
        <FontAwesomeIcon icon={faSearch}/>
      </button>
    </div>


        <table className='table table-bordered'>
        <thead>
          <tr>
            <th>Nombre</th>
            <th>Apellido</th>
            <th>Apodo</th>
            <th>Edad</th>
            <th>Debilidad</th>
            <th>Habilidad</th>
            <th>Origen</th>
          </tr>
        </thead>
        <tbody>
          {data.map(db=>(
          <tr key={db.id}>
            <td>{db.persona.nombre}</td>
            <td>{db.persona.apellido}</td>
            <td>{db.persona.apodo}</td> 
            <td>{db.persona.edad}</td> 
            <td>{db.persona.debilidad}</td> 
            <td>{db.persona.habilidadPoder}</td>
            <td>{db.origen}</td>
          </tr>
          ))}
        </tbody>
      </table>

        <Button 
      onClick = {() => {
        
        setModalOpen(true)
      }} className='btn btn-success'>Agregar</Button>

      < Modal isOpen={modalOpen}>
        
        <ModalHeader>Información del villano</ModalHeader>
        <ModalBody>
            <div className='form-group'>
                <label> Nombre </label>
                <br />
                <input type="text" className='form-control' name='nombre' onChange={handleChange}/>
                <br />
                <label> Apellido </label>
                <input type="text" className='form-control' name='apellido' onChange={handleChange}/>
                <br />
                <label> Apodo </label>
                <input type="text" className='form-control' name='apodo' onChange={handleChange}/>
                <br />
                <label> Edad </label>
                <input type="text" className='form-control' name='edad' onChange={handleChange}/>
                <br />
                <label> Debilidad </label>
                <input type="text" className='form-control' name='debilidad' onChange={handleChange}/>
                <br />
                <label> Habilidad </label>
                <input type="text" className='form-control' name='habilidadPoder' onChange={handleChange}/>
                <br />
                <label> Origen </label>
                <input type="text" className='form-control' name='origen' onChange={handleChange}/>
                <br />

            </div>


        </ModalBody>
        
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => PostVillanos()}>Insertar</button>  
          <button className="btn btn-danger" onClick={()=> setModalOpen(false)} >Salir</button>
        </ModalFooter>
        
      </Modal>
    </div>
 );
} 

export default Villanos;
