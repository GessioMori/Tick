import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  const [name, setName] = useState("");

  const login = async () => {
    const response = await fetch("https://localhost:5001/api/account/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            email: "test4@mail.com",  
            password: "#312312312",    
        }),
        credentials: "include", 
    });

    if (response.ok) {
        console.log("Login successful");
    } else {
        console.error("Login failed", await response.text());
    }
  };

  const whoAmI = async () => {
    const response = await fetch("https://localhost:5001/api/account/whoami", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include", 
    });

    if (response.ok) {
      const resName = await response.text();
      setName(resName);
    } else {
        console.error("WhoAmI failed", await response.text());
    }
  };

  useEffect(() => {
    login();
    whoAmI();
  },[]);

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Hello, {name}</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </>
  )
}

export default App
