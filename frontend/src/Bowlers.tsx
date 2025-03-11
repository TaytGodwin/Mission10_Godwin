import { useEffect, useState } from 'react';
import { Bowler } from './types/Bowler';

function BowlerList() {
  const [bowlers, setBowlers] = useState<Bowler[]>([]);

  useEffect(() => {
    const fetchBowlers = async () => {
      const response = await fetch('https://localhost:5000/api/Bowler');
      const data = await response.json();
      setBowlers(data);
    };
    fetchBowlers();
  }, []);

  return (
    <>
      <table>
        <thead>
          <tr>
            <th>Bowler Name</th>
            <th>Team Name</th>
            <th>Bowler Address</th>
            <th>Bowler City</th>
            <th>Bowler State</th>
            <th>Bowler Zip</th>
            <th>Bowler Phone Number</th>
          </tr>
        </thead>
        <tbody>
          {bowlers.map((b) => (
            <tr key={b.bowlerId}>
              <td>
                {b.bowlerFirstName} {b.bowlerMiddleInit} {b.bowlerLastName}
              </td>
              <td>{b.teamName}</td>
              <td>{b.bowlerAddress}</td>
              <td>{b.bowlerCity}</td>
              <td>{b.bowlerState}</td>
              <td>{b.bowlerZip}</td>
              <td>{b.bowlerPhoneNumber}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
}

export default BowlerList;
