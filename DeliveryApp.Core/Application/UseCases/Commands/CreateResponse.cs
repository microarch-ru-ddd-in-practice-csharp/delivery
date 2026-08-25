using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryApp.Core.Application.UseCases.Commands;

public class CreateResponse
{
    public Guid Id { get; }

    public bool Ok { get; }

    public static CreateResponse Failure => new CreateResponse();

    private CreateResponse()
    {
        Ok = false;
        Id = Guid.Empty;
    }

    public CreateResponse(Guid id)
    {
        Id = id;
        Ok = true;
    }
}
