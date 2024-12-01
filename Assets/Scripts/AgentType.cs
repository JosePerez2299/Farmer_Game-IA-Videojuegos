using System;
using System.Collections.Generic;

public enum AgentType
{
    Farmer,
    Bird,
    None,
}

public class AgentTypeUtility
{
    public AgentType GetAgentTypeFromString(string name)
    {
        // Intenta convertir el string al enum
        if (Enum.TryParse(name, false, out AgentType agentType))
        {
            return agentType;
        }

        // Si no se encuentra un valor válido, lanza una excepción o retorna un valor predeterminado
        return AgentType.None;
    }
}
