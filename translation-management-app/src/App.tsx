import { Container } from 'react-bootstrap';
import { BrowserRouter, Route, Routes, } from 'react-router-dom';
import Create from './components/create.translator.component';
import Index from './components/index.translator.component';
import Edit from './components/edit.translator.component';
import { ToastContainer } from "react-toastify"
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <Container>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Index />} />
          <Route path="/create" element={<Create />} />
          <Route path="/edit/:id" element={<Edit />} />
        </Routes>
      </BrowserRouter>
      <ToastContainer />
    </Container>
  );
}

export default App;
