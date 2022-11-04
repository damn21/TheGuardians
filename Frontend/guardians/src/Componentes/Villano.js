import React, {useState,useEffect} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import '../App.css'
import { Button } from 'react-bootstrap';
import {Modal,ModalBody,ModalFooter,ModalHeader,FormGroup, Input, Label, Form} from 'reactstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import swal from 'sweetalert'

function Villanos () {

    const VillanosUrl = "https://localhost:7164/api/villanos";
    const [data, setData] = useState([]);

    const [tablaUsuarios, setTablaUsuarios]= useState([]);
    const [busqueda, setBusqueda]= useState("");

    const [modalOpen, setModalOpen] = useState(false);
    const [nombre, setNombre] = useState('');
    const [apellido, setApellido] = useState('');
    const [apodo, setApodo] = useState('');
    const [edad, setEdad] = useState(0);
    const [habilidades, setHabilidades] = useState('');
    const [debilidades, setDebilidades] = useState('');
    const [origen, setOrigen] = useState('');

    // const [Gestor,setGestor] =  useState({
    //     origen:'',
    //     persona:{
    //       nombre:'',
    //       apellido:'',
    //       apodo:'',
    //       edad: '',
    //       debilidad:'',
    //       habilidadPoder:'',
    //       pais:''
    //     }
    // })

    // const handleChange = i => {
    //     const {name, value} = i.target;
    //     setGestor({
    //         ...Gestor,
    //         [name]:value
    //     });
    //     console.log(Gestor);
    // }


    const GetVillanos = async () => {
      await axios.get(VillanosUrl)
      .then(response => {
        setData(response.data);
        setTablaUsuarios(response.data);
        console.log(response.data);
      }).catch(error => {
        console.log(error);
      })
    }

    const PostVillano = async (e)=>{
      e.preventDefault()
        swal({
          title: "Registrado correctamente",
          //text: "Redirecting in 2 seconds.",
          icon: "success",
          timer: 4000,
          button: true
        });
         await axios.post(VillanosUrl, {
          "origen": origen,
          "persona": {
            "nombre": nombre,
            "apellido": apellido,
            "apodo": apodo,
            "edad": edad,
            "debilidad": debilidades,
            "habilidadPoder": habilidades
          }
        });
          setModalOpen(false);
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
        onChange={handleChange2}/>
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
            <th>Pais</th>
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
            <td>{db.persona.pais}</td> 
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

      <Modal isOpen={modalOpen} >
        <ModalHeader toggle={()=> setModalOpen(true)}>
        Registrar villano
        </ModalHeader>
        <ModalBody>
          <Form className="form" onSubmit={(e) => PostVillano(e)}>
            <FormGroup>
              <Label for="nombre">Nombre</Label>
              <Input type='text' id='nombre' value={nombre}  onChange={(e)=>(setNombre(e.target.value))}>
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="apellido">Apellido</Label>
              <Input type='text' id='apellido' value={apellido}  onChange={(e)=>(setApellido(e.target.value))}>
              </Input>
            </FormGroup>
            <FormGroup>
              <Label for="apodo">Apodo</Label>
              <Input type='text' id='apodo' value={apodo}  onChange={(e)=>(setApodo(e.target.value))}>
              </Input>
            </FormGroup>
            <FormGroup>
             <Label for="edad">Edad</Label>
             <Input type="number" id="edad" value={edad}  onChange={(e)=>setEdad(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
            <Label for="habilidades">Habilidades</Label>
             <Input type='text' id='habilidades' value={habilidades} onChange={(e)=>setHabilidades(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
            <Label for="debilidades">Debilidades</Label>
             <Input type='text' id='debilidades' value={debilidades} onChange={(e)=>setDebilidades(e.target.value)}></Input>
            </FormGroup>
            <FormGroup>
            <Label for="origen">Origen</Label>
             <Input type='text' id='origen' value={origen} onChange={(e)=>setOrigen(e.target.value)}></Input>
            </FormGroup>
            <Button variant="success" type="submit">Añadir</Button>{' '}
            <Button variant="danger" onClick={()=> setModalOpen(false)}>Salir</Button>
          </Form>
        </ModalBody>
        <ModalFooter>
            <h5 className='text-danger small'></h5>      
        </ModalFooter>
     </Modal>
        
    </div>
 );
} 

export default Villanos;
