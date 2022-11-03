import React, {useState,useEffect} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import '../App.css'
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import { MDBContainer } from "mdb-react-ui-kit";

function Heroe() {

  const getHeroesUrl = "https://localhost:7164/api/heroes";
  const getHeroesAdostes = "https://localhost:7164/api/heroes/edad/adolescentes"
  const getHeroesMayores = "https://localhost:7164/api/heroes/edad/mayores"
  const getHeroesNombre = "https://localhost:7164/api/heroes/nombre?nombre"

  const [data, setData] = useState([]);
  const [data1, setData1] = useState([]);
  const [data2, setData2] = useState([]);
  const [modalOpen, setModalOpen] = useState(false);
  const [modalOpen1, setModalOpen1] = useState(false);

  // const [searchParams, setSearchParams] = useSearchParams();

  const [tablaUsuarios, setTablaUsuarios]= useState([]);
  const [busqueda, setBusqueda]= useState("");

  const handleClose = () => setModalOpen(false);
  const handleShow = () => setModalOpen(true);

  const handleClose1 = () => setModalOpen1(false);
  const handleShow1 = () => setModalOpen1(true);

  const GetHeroes = async () => {
    await axios.get(getHeroesUrl)
    .then(response => {
      setData(response.data);
      setTablaUsuarios(response.data);
      console.log(response.data);
    }).catch(error => {
      console.log(error);
    })
  }

  // const GetHeroesNombre = async () => {
  //   await axios.get(getHeroesNombre)
  //   .then(response => {
  //     setTablaUsuarios(response.data);
  //     console.log(response.data);
  //   }).catch(error => {
  //     console.log(error);
  //   })
  // }

  const GetAdolescentes = async () => {
    await axios.get(getHeroesAdostes)
    .then(response => {
      setData1(response.data);
      console.log(response.data);
    }).catch(error => {
      console.log(error);
    })
  }

  const GetMayores = async () => {
    await axios.get(getHeroesMayores)
    .then(response => {
      setData2(response.data);
      console.log(response.data);
    }).catch(error => {
      console.log(error);
    })
  }

  const handleChange=e=>{
    setBusqueda(e.target.value);
    filtrar(e.target.value);
  }
  
  const filtrar=(terminoBusqueda)=>{
    var resultadosBusqueda=tablaUsuarios.filter((elemento)=>{
      if( elemento.idPersonaNavigation.nombre.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
      || elemento.idPersonaNavigation.habilidadPoder.toString().toLowerCase().includes(terminoBusqueda.toLowerCase())
      )
      {
        return elemento;
      }
    });
    setData(resultadosBusqueda);
  }

  useEffect(()=> {
    GetHeroes();
    GetAdolescentes();
    GetMayores();
    // GetHeroesNombre();
  },[])

  return (

    <div className="App" >
      <div className="containerInput">
        <input
        className="form-control inputBuscar"
        value={busqueda}
        placeholder="Búsqueda por Apodo, Habilidades o Relaciones personales"
        onChange={handleChange}
      />

      <button className="btn btn-success">
        <FontAwesomeIcon icon={faSearch}/>
      </button>
    </div>
        {/* <MDBContainer className="py-5">
      <input
        type={busqueda}
        className="search-hover"
        placeholder="search here..."
        onChange={handleChange}
      />
    </MDBContainer> */}

    <div className="table-responsive">
    <table className='table table-bordered'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Nombre</th>
            <th>Apellido</th>
            <th>Apodo</th>
            <th>Edad</th>
            <th>Debilidad</th>
            <th>Habilidad</th>
            <th>Relaciones personales</th>
          </tr>
        </thead>
        <tbody>
          {data.map(db=>(
          <tr key={db.id}>
            <td>{db.heroeId}</td>
            <td>{db.idPersonaNavigation.nombre}</td>
            <td>{db.idPersonaNavigation.apellido}</td>
            <td>{db.idPersonaNavigation.apodo}</td> 
            <td>{db.idPersonaNavigation.edad}</td> 
            <td>{db.idPersonaNavigation.debilidad}</td> 
            <td>{db.idPersonaNavigation.habilidadPoder}</td>
            <td>{db.contactoPersonals.map(c => c.nombre)+'\n'}</td>
          </tr>
          ))}
        </tbody>
      </table>

    </div>

      <Button onClick={handleShow}
      className='btn btn-success'>Heroes adolescentes</Button>{'  '}
      <Button onClick={handleShow1}
      className='btn btn-success'>Heroes mayores</Button>

      <Modal 
      show={modalOpen}
      onHide={handleClose}
      size="sm"
      aria-labelledby="example-modal-sizes-title-sm" >
        <Modal.Header closeButton>
          <Modal.Title>Lista de Héroes adolescentes</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <table className='table table-bordered'>
            <thead>
                <tr>
                    <th>Heroe</th>
                    <th>Edad</th>
                </tr>
            </thead>
        
        <tbody>
          {data1.map(db=>(
          <tr key={db.id}>
            <td>{db.apodo}</td>
            <td>{db.edad}</td>
          </tr>
          ))}
        </tbody>
      </table>   
        </Modal.Body>
      </Modal>

      <Modal 
      show={modalOpen1}
      onHide={handleClose1}
      size="sm"
      aria-labelledby="example-modal-sizes-title-sm" 
      >
        <Modal.Header closeButton>
          <Modal.Title> Lista de Héroes mayores</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <table className='table table-bordered'>
            <thead>
                <tr>
                    <th>Heroe</th>
                    <th>Edad</th>
                </tr>
            </thead>
        
        <tbody>
          {data2.map(db=>(
          <tr key={db.id}>
            <td>{db.apodo}</td>
            <td>{db.edad}</td>
          </tr>
          ))}
        </tbody>
      </table>
          
        </Modal.Body>

      </Modal>



    </div> 
  );
}

export default Heroe;