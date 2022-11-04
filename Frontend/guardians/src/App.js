import './App.css';
import Heroe from './Componentes/Heroe'
import Villano from './Componentes/Villano'
import Combates from './Componentes/Combates'
import Patrocinadores from './Componentes/Patrocinadores';
import NavBarExample from './Componentes/NavBar'
import { BrowserRouter, Route, Routes } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      
      <BrowserRouter>
        <Routes >
          <Route path='/' element={ <NavBarExample /> }>
            <Route path='heroes' element={ <Heroe />} />
            <Route path='villanos' element={ <Villano/> } />
            <Route path='combates' element={ <Combates/> } />
            <Route path='patrocinador' element={ <Patrocinadores/> } />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}
 
export default App;
