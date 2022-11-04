import React, {useState,useEffect} from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import '../App.css'
import {Button,Modal,ModalBody,ModalFooter,ModalHeader} from 'reactstrap';

function Combates() {

    const getCombatesUrl = "https://localhost:7164/api/combate";
    const getTopVictoriasHUrl = "https://localhost:7164/api/combate/heroes/victorias";
    const getDerrotasVillanoUrl = "https://localhost:7164/api/combate/villano/derrota";
    const [data, setData] = useState([]);
    const [data1, setData1] = useState([]);
    const [data2, setData2] = useState([]);
    const [modalOpen, setModalOpen] = useState(false);
    const [modalOpen1, setModalOpen1] = useState(false);

    const GetCombates = async () => {
      await axios.get(getCombatesUrl)
      .then(response => {
        setData(response.data);
        console.log(response.data);
      }).catch(error => {
        console.log(error);
      })
    }

    const GetTop = async () => {
        await axios.get(getTopVictoriasHUrl)
        .then(response => {
          setData1(response.data);
          console.log(response.data);
        }).catch(error => {
          console.log(error);
        })
      }

      const GetDerrotas = async () => {
        await axios.get(getDerrotasVillanoUrl)
        .then(response => {
          setData2(response.data);
          console.log(response.data);
        }).catch(error => {
          console.log(error);
        })
      }

    useEffect(()=> {
        GetCombates();
        GetTop();
        GetDerrotas();
    },[])

    return (
      
      <div className='wrapper' >
        
  <table className='table table-bordered'>
          <thead>
            <tr>
              <th>Heroe</th>
              <th>Villano</th>
              <th>Resultado</th>
            </tr>
          </thead>
          <tbody>
            {data.map(db=>(
            <tr key={db.id}>
              <td>{db.apodoH}</td>
              <td>{db.apodo}</td>
              <td>{db.resultado}</td> 
            </tr>
            ))}
          </tbody>
        </table>

        <Button 
        onClick = {() => {
        setModalOpen(true)
      }} className='btn btn-success'>Top 3</Button>{' '}
      
      <Button 
        onClick = {() => {
        setModalOpen1(true)
      }} className='btn btn-success'>Villano derrota</Button>

        <Modal isOpen={modalOpen} >
            <ModalHeader>Top 3 héroes con mayor número de victorias</ModalHeader>
            <ModalBody>
            <table className='table table-bordered'>
          <thead>
            <tr>
              <th>Heroe</th>
              <th>Victorias</th>
            </tr>
          </thead>
          <tbody>
            {data1.map(db=>(
            <tr key={db.id}>
              <td>{db.heroe}</td>
              <td>{db.victorias}</td>
            </tr>
            ))}
          </tbody>
        </table>
            </ModalBody>

            <ModalFooter>
               <button className='btn btn-danger' onClick={()=> setModalOpen(false)}>Salir</button> 
            </ModalFooter>
        </Modal>

        <Modal isOpen={modalOpen1} >
            <ModalHeader>Villano con mayor número de derrotas por héroe de menor
      edad</ModalHeader>
            <ModalBody>
            <table className='table table-bordered'>
          <thead>
            <tr>
              <th>Villano</th>
              <th>Heroe</th>
              <th>Edad heroe</th>
              <th># Derrotas</th>
            </tr>
          </thead>
          <tbody>
            {data2.map(db=>(
            <tr key={db.id}>
              <td>{db.nombre_Villano}</td>
              <td>{db.nombre_Heroe}</td>
              <td>{db.edad_Heroe}</td>
              <td>{db.count}</td>
            </tr>
            ))}
          </tbody>
        </table>
            </ModalBody>

            <ModalFooter>
               <button className='btn btn-danger' onClick={()=> setModalOpen1(false)}>Salir</button> 
            </ModalFooter>
        </Modal>



      </div> 
    );
  }

  
  
  export default Combates;